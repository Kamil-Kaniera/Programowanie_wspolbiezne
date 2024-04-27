using Pool.Common.Model;

namespace Pool.Logic.Abstract;

public interface ILogicApi
{
    IEnumerable<Ball> Balls { get; }
    void CreateBalls(int numberOfBalls);
    void StartMovement(Action<Guid> callback);
    Task StopMovement();
    Ball GetBall(Guid ballId);
}