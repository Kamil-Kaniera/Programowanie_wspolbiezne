using Pool.Common.Model;
using Pool.Data.Abstract;
using Pool.Logic.Abstract;

namespace Pool.Logic.Implementation;

public class LogicApi : ILogicApi
{
    private const int MoveIntervalMs = 10;

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

        for (var i = 0; i < numberOfBalls; i++)
        {
            // Randomizuj początkową pozycję
            int randomizedX, randomizedY;
            do
            {
                randomizedX = _rnd.Next(0, _table.Size.Y - (Constants.DIAMETER * Constants.RESCALE));
                randomizedY = _rnd.Next(0, _table.Size.X - (Constants.DIAMETER * Constants.RESCALE));
            } while (IsBallIntersectingAnyOther(randomizedX, randomizedY, Balls));

            var randomizedDirection = new DirectionVector();

            Ball ball = new()
            {
                BallId = Guid.NewGuid(),
                Position = new() { X = randomizedX, Y = randomizedY },
                Direction = randomizedDirection
            };

            _dataApi.AddBall(ball);
        }
    }

    private bool IsBallIntersectingAnyOther(int x, int y, IEnumerable<Ball> existingBalls)
    {
        foreach (var existingBall in existingBalls)
        {
            lock (existingBall)
            {
                if (Math.Sqrt((existingBall.Position.X - x) * (existingBall.Position.X - x) +
                              (existingBall.Position.Y - y) * (existingBall.Position.Y - y)) <=
                    existingBall.Diameter) // Kulki się stykają
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
            _tasks.Add(MoveBallRandomly(ball, ballMovedCallback));
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

    private async Task MoveBallRandomly(Ball ball, Action<Guid> callback)
    {
        while (_movement)
        {
            MoveBall(ball);

            callback(ball.BallId);

            await Task.Delay(MoveIntervalMs);
        }
    }

    public void MoveBall(Ball ball)
    {
        var newX = ball.Position.X + ball.Direction.X;
        var newY = ball.Position.Y + ball.Direction.Y;
        var oldPosition = ball.Position;
        ball.Position = new() { X = newX, Y = newY };

        if (CheckBallCollision(ball))
        {
            ball.Position = new (oldPosition.X + ball.Direction.X, oldPosition.Y + ball.Direction.Y);
        }

        CheckWallCollision(ball);

        _dataApi.UpdateBall(ball);
    }

    public void CheckWallCollision(Ball ball)
    {
        if (ball.Position.X <= 0 || ball.Position.X >= _table.Size.X - ball.Diameter)
            ball.Direction.X = - ball.Direction.X;
        
        if (ball.Position.Y <= 0 || ball.Position.Y >= _table.Size.Y - ball.Diameter)
            ball.Direction.Y = - ball.Direction.Y;
        
    }

    public bool CheckBallCollision(Ball ball)
    {
        bool collisionDetected = false;

        foreach (var b in Balls)
        {
            if (b.BallId == ball.BallId) continue;

            var distance = Math.Sqrt((b.Position.X - ball.Position.X) * (b.Position.X - ball.Position.X) +
                                     (b.Position.Y - ball.Position.Y) * (b.Position.Y - ball.Position.Y));


            if (distance <= ball.Diameter) // Kulki się stykają
            {
                // Umieść kule w sekcji krytycznej
                lock (b)
                {
                    lock (ball)
                    {
                        HandleCollision(b, ball);
                    }
                }
                collisionDetected = true;
            }
        }

        return collisionDetected;
    }


    public void HandleCollision(Ball ball1, Ball ball2)
    {
        double normalX = ball2.Position.X - ball1.Position.X;
        double normalY = ball2.Position.Y - ball1.Position.Y;
        double normalLength = Math.Sqrt((normalX * normalX + normalY * normalY));
        normalX /= normalLength;
        normalY /= normalLength;

        double dotProduct1 = ball1.Direction.X * normalX + ball1.Direction.Y * normalY;
        double dotProduct2 = ball2.Direction.X * normalX + ball2.Direction.Y * normalY;

        double newVx1 = ball1.Direction.X - 2 * dotProduct1 * normalX;
        double newVy1 = ball1.Direction.Y - 2 * dotProduct1 * normalY;
        double newVx2 = ball2.Direction.X - 2 * dotProduct2 * normalX;
        double newVy2 = ball2.Direction.Y - 2 * dotProduct2 * normalY;

        ball1.Direction.X = (int)newVx1;
        ball1.Direction.Y = (int)newVy1;
        ball2.Direction.X = (int)newVx2;
        ball2.Direction.Y = (int)newVy2;
    }



}