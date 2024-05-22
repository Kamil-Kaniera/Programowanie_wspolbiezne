using Logic.Abstract;
using Commons;
using Data.Abstract;

namespace Logic.Implementation
{
    public class LogicApi(IDataApi data) : ILogicApi
    {
        public List<ILogicBall> LogicBalls { get; } = [];
        private IDataApi _dataApi = data;

        private readonly Random _rnd = new();

        private readonly Object _ballLock = new();

        public void OnCompleted() { }

        public void OnError(Exception error) { }

        public void OnNext(IBall value)
        {
          //  lock (value)
            {
                CheckCollision((IBall)value);
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
                    randomizedX = _rnd.Next(0, Constants.TABLE_X - Constants.DIAMETER) * Constants.RESCALE;
                    randomizedY = _rnd.Next(0, Constants.TABLE_Y - Constants.DIAMETER) * Constants.RESCALE;
                } while (IsBallIntersectingAnyOther(randomizedX, randomizedY, LogicBalls));


                IBall ball = _dataApi.AddBall(new Position(randomizedX, randomizedY));
                LogicBall logicBall = new(new(randomizedX, randomizedY));

                LogicBalls.Add(logicBall);

                ball.Subscribe(logicBall);
                ball.Subscribe(this);

            }
        }

        private static bool IsBallIntersectingAnyOther(int x, int y, IList<ILogicBall> existingBalls)
        {
            foreach (var existingBall in existingBalls)
            {
               // lock (existingBall)
                {
                    if (Math.Sqrt((existingBall.Position.X - x) * (existingBall.Position.X - x) +
                                  (existingBall.Position.Y - y) * (existingBall.Position.Y - y)) <=
                        existingBall.Diameter) // Balls are touching
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private void CheckCollision(IBall ball)
        {
           // lock (_ballLock)
            {
                foreach (var b in _dataApi.Balls)
                {
                
                    if (b.Position.X == ball.Position.X && b.Position.Y == ball.Position.Y) continue;

                    var distance = Math.Sqrt((b.Position.X - ball.Position.X) * (b.Position.X - ball.Position.X) +
                                             (b.Position.Y - ball.Position.Y) * (b.Position.Y - ball.Position.Y));


                    if (distance <= ball.Diameter)
                    {
                        HandleBallCollision(b, ball);
                    }
                }
                

                HandleWallCollision(ball);
            }
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
            if (ball.Position.X <= 0)
                ball.Velocity = new(Math.Abs(ball.Velocity.X), ball.Velocity.Y);
            if (ball.Position.Y <= 0)
                ball.Velocity = new(ball.Velocity.X, Math.Abs(ball.Velocity.Y));

            if (ball.Position.X >= _dataApi.GetTable().TableSize.X - ball.Diameter)
                ball.Velocity = new(-Math.Abs(ball.Velocity.X), ball.Velocity.Y);
            if (ball.Position.Y >= _dataApi.GetTable().TableSize.Y - ball.Diameter)
                ball.Velocity = new(ball.Velocity.X, -Math.Abs(ball.Velocity.Y));
        }

        public void StopMovement()
        {
            LogicBalls.Clear();
            _dataApi.RemoveAllBalls();
        }
    }
}
