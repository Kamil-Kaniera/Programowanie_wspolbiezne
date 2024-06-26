﻿using Data;
using Data.Abstract;
using Data.Implementation;
using Logic.Abstract;

namespace Logic.Implementation
{
    public class LogicApi : ILogicApi
    {
        public List<ILogicBall> LogicBalls { get; } = new();
        private List<(Guid, Guid)> BallsId = new();

        private readonly IDataApi _dataApi;

        public LogicApi()
        {
            _dataApi = new DataApi();
        }

        public LogicApi(IDataApi data)
        {
            _dataApi = data;
        }

        private const int Rescale = 100;
        private const int Diameter = 20;
        private const int TableX = 500;
        private const int TableY = 500;

        private readonly Random _rnd = new();

        private readonly object _lock = new();
        private readonly object _ballsLock = new();

        public void OnCompleted() { }

        public void OnError(Exception error) { }

        public void OnNext(IBall value)
        {
            lock (_lock)
            {
                CheckCollision(value);
            }
        }

        public void StartMovement(int numberOfBalls)
        {
            LogicBalls.Clear();
            _dataApi.RemoveAllBalls();

            for (var i = 0; i < numberOfBalls; i++)
            {
                // Randomize starting position
                int randomizedX, randomizedY;
                do
                {
                    randomizedX = _rnd.Next(0, TableX - Diameter) * Rescale;
                    randomizedY = _rnd.Next(0, TableY - Diameter) * Rescale;
                } while (IsBallIntersectingAnyOther(randomizedX, randomizedY, LogicBalls));

                var position = new Position(randomizedX, randomizedY);

                IBall ball = _dataApi.AddBall(position);
                LogicBall logicBall = new(new LogicPosition(randomizedX, randomizedY));

                LogicBalls.Add(logicBall);

                ball.Subscribe(logicBall);
                ball.Subscribe(this);
            }
        }

        private bool IsBallIntersectingAnyOther(int x, int y, IList<ILogicBall> existingBalls)
        {
            lock (_ballsLock)
            {
                foreach (var existingBall in existingBalls)
                {
                    if (Math.Sqrt((existingBall.Position.X - x) * (existingBall.Position.X - x) +
                                  (existingBall.Position.Y - y) * (existingBall.Position.Y - y)) <=
                        Diameter * Rescale) // Balls are touching
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private void CheckCollision(IBall ball)
        {
            List<IBall> ballsCopy;

            lock (_ballsLock)
            {
                ballsCopy = new List<IBall>(_dataApi.Balls);
            }

            foreach (var b in ballsCopy)
            {
                if (b?.Position == null || ball.Position == null) continue;
                if (b.Position.X == ball.Position.X && b.Position.Y == ball.Position.Y) continue;

                var distance = Math.Sqrt((b.Position.X - ball.Position.X) * (b.Position.X - ball.Position.X) +
                                         (b.Position.Y - ball.Position.Y) * (b.Position.Y - ball.Position.Y));

                if (distance <= Diameter * Rescale)
                {
                    var pair = (ball.BallId, b.BallId);
                    var reversePair = (b.BallId, ball.BallId);

                    if (BallsId.Contains(pair) || BallsId.Contains(reversePair))
                    {
                        // If exact pair already exists, do nothing
                        continue;
                    }
                    
                    // Remove any existing pairs containing ball.BallId or b.BallId
                    BallsId.RemoveAll(p => p.Item1 == ball.BallId || p.Item2 == ball.BallId || p.Item1 == b.BallId || p.Item2 == b.BallId);

                    // Add new pair
                    BallsId.Add(pair);
                    
                    HandleBallCollision(b, ball);
                }
            }
            HandleWallCollision(ball);
        }

        private void HandleBallCollision(IBall ball, IBall otherBall)
        {
            double normalX = otherBall.Position.X - ball.Position.X;
            double normalY = otherBall.Position.Y - ball.Position.Y;
            double normalLength = Math.Sqrt((normalX * normalX + normalY * normalY));
            normalX /= normalLength;
            normalY /= normalLength;

            double dotProduct1 = ball.Velocity.X * normalX + ball.Velocity.Y * normalY;
            double dotProduct2 = otherBall.Velocity.X * normalX + otherBall.Velocity.Y * normalY;

            double newVx1 = ball.Velocity.X - 2 * dotProduct1 * normalX;
            double newVy1 = ball.Velocity.Y - 2 * dotProduct1 * normalY;
            double newVx2 = otherBall.Velocity.X - 2 * dotProduct2 * normalX;
            double newVy2 = otherBall.Velocity.Y - 2 * dotProduct2 * normalY;

            ball.Velocity = new((int)newVx1, (int)newVy1);
            otherBall.Velocity = new((int)newVx2, (int)newVy2);
        }

        private void HandleWallCollision(IBall ball)
        {
            bool collisionOccurred = false;

            if (ball.Position.X <= 0 || ball.Position.X >= _dataApi.GetTable().TableX - Diameter * Rescale)
            {
                ball.Velocity = new(Math.Abs(ball.Velocity.X) * (ball.Position.X <= 0 ? 1 : -1), ball.Velocity.Y);
                collisionOccurred = true;
            }
            if (ball.Position.Y <= 0 || ball.Position.Y >= _dataApi.GetTable().TableY - Diameter * Rescale)
            {
                ball.Velocity = new(ball.Velocity.X, Math.Abs(ball.Velocity.Y) * (ball.Position.Y <= 0 ? 1 : -1));
                collisionOccurred = true;
            }

            if (collisionOccurred)
            {
                BallsId.RemoveAll(p => p.Item1 == ball.BallId || p.Item2 == ball.BallId);
            }
        }

        public void StopMovement()
        {
            LogicBalls.Clear();
            _dataApi.RemoveAllBalls();
        }
    }
}
