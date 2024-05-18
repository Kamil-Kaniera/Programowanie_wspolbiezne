using Pool.Common.Model;
using Pool.Data.Abstract;
using Pool.Logic.Abstract;
using System.Diagnostics;

namespace Pool.Logic.Implementation;

public class LogicApi : ILogicApi
{
    private readonly IDataApi _dataApi;
    private readonly Random _rnd = new();
    private readonly Table _table;

    private readonly List<Task> _tasks = [];

    private bool _movement;

    public LogicApi(IDataApi dataApi)
    {
        _dataApi = dataApi;
        _table = _dataApi.GetTable();
    }

    public IEnumerable<Ball> Balls => _dataApi.Balls;

    public Ball GetBall(Guid ballId)
    {
        return _dataApi.GetBall(ballId);
    }

    public void CreateBalls(int numberOfBalls)
    {
        _dataApi.RemoveAllBalls();

        for (var i = 0; i < numberOfBalls; i++)
        {
            // Randomize starting position
            int randomizedX, randomizedY;
            do
            {
                randomizedX = _rnd.Next(0, Constants.TABLE_X - Constants.DIAMETER) * Constants.RESCALE;
                randomizedY = _rnd.Next(0, Constants.TABLE_Y - Constants.DIAMETER) * Constants.RESCALE;
            } while (IsBallIntersectingAnyOther(randomizedX, randomizedY, Balls));


            Ball ball = new()
            {
                BallId = Guid.NewGuid(),
                Position = new(randomizedX, randomizedY),
                Direction = new()
            };

            _dataApi.AddBall(ball);
        }
    }

    private static bool IsBallIntersectingAnyOther(int x, int y, IEnumerable<Ball> existingBalls)
    {
        foreach (var existingBall in existingBalls)
        {
            lock (existingBall)
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


    public void StartMovement(Action<Guid> ballMovedCallback)
    {
        _movement = true;
        foreach (var ball in _dataApi.Balls)
            _tasks.Add(CreateTask(ball, ballMovedCallback));
    }

    public async Task StopMovement()
    {
        _movement = false;

        // Wait every task to be completed 
        await Task.WhenAll(_tasks);

        // Remove balls in data layer
        _dataApi.RemoveAllBalls();

        // Clear the current logic layer list
        _tasks.Clear();
    }

    private async Task CreateTask(Ball ball, Action<Guid> callback)
    {
        while (_movement)
        {
            await ball.MoveBallRandomly(MoveBall, callback);
        }
    }

    private void MoveBall(Ball ball)
    {
        // Lock the ball before any changes
        lock (ball)
        {
            var oldPosition = ball.Position;

            ball.MoveSelf();

            if (CheckBallCollision(ball))
            {
                ball.Position = new(oldPosition.X, oldPosition.Y);
                ball.MoveSelf();
            }

            ball.HandleWallCollision(_table);

            _dataApi.UpdateBall(ball);
        }
    }

    private bool CheckBallCollision(Ball ball)
    {
        bool collisionDetected = false;

        foreach (var b in Balls)
        {
            // Lock the other ball
            lock (b)
            {
                if (b.BallId == ball.BallId) continue;

                var distance = Math.Sqrt((b.Position.X - ball.Position.X) * (b.Position.X - ball.Position.X) +
                                         (b.Position.Y - ball.Position.Y) * (b.Position.Y - ball.Position.Y));


                if (distance <= ball.Diameter)
                {
                    ball.HandleBallCollision(b);

                    collisionDetected = true;
                }
            }
        }

        return collisionDetected;
    }
}