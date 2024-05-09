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
        _dataApi.RemoveAllBalls();
        for (var i = 0; i < numberOfBalls; i++)
        {
            // Randomize starting position
            var randomizedX = _rnd.Next(0, _table.Size.Y - Constants.RADIUS);
            var randomizedY = _rnd.Next(0, _table.Size.X - Constants.RADIUS);
            var randomizedDirection = new DirectionVector();

            Ball ball = new()
            {
                BallId = Guid.NewGuid(), Position = new() { X = randomizedX, Y = randomizedY },
                Direction = randomizedDirection
            };

            _dataApi.AddBall(ball);
        }
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
            MoveBall(ball, ball.Direction.X, ball.Direction.Y);

            callback(ball.BallId);

            await Task.Delay(MoveIntervalMs);
        }
    }

    public void MoveBall(Ball ball, int x, int y)
    {
        var newX = ball.Position.X + x;
        var newY = ball.Position.Y + y;
        ball.Position = new() { X = newX, Y = newY };

        Bounce(ball);

        _dataApi.UpdateBall(ball);
    }

    private void Bounce(Ball ball)
    {
        if (CheckWallCollision(ball))
           ball.Direction.Invert();
        if (CheckBallCollision(ball))
           ball.Direction.Invert();
    }

    public bool CheckWallCollision(Ball ball)
    {
        return ball.Position.X <= 0 || ball.Position.X >= _table.Size.X - Constants.RADIUS
                      || ball.Position.Y <= 0 || ball.Position.Y >= _table.Size.Y - Constants.RADIUS;
    }

    public bool CheckBallCollision(Ball ball)
    {
        foreach (var b in Balls)
        {
            if (b.BallId == ball.BallId) continue;
            var distance = Math.Sqrt(Math.Pow(b.Position.X - ball.Position.X, 2) + Math.Pow(b.Position.Y - ball.Position.Y, 2));

            if (distance < Constants.RADIUS)
            {
                return true;
            }
        }

        return false;
    }

    
}