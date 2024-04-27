using Pool.Common.Model;

namespace Pool.Data.Abstract;

public interface IDataApi
{
    IEnumerable<Ball> Balls { get; }
    void AddBall(Ball ball);
    void RemoveAllBalls();
    void UpdateBall(Ball ball);
    Table GetTable();
    Ball GetBall(Guid ballId);
}