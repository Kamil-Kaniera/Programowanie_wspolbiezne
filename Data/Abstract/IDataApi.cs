using Commons;

namespace Data.Abstract
{
    public interface IDataApi
    {
        IBall AddBall(Position p);
        void RemoveAllBalls();
        ITable GetTable();
        List<IBall> Balls { get; }
    }
}
