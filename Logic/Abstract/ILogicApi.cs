using Data.Abstract;

namespace Logic.Abstract
{
    public interface ILogicApi : IObserver<IBall>
    {
        void StartMovement(int numberOfBalls);
        void StopMovement();
        List<ILogicBall> LogicBalls { get; }
    }
}
