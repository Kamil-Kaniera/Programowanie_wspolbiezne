using Data;
using Data.Abstract;

namespace Logic.Abstract
{
    public interface ILogicBall : IObserver<IBall>, IObservable<ILogicBall>
    {
       Position Position { get; }
    }
}
