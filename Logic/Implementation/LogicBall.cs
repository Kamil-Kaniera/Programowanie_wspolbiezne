using Data;
using Data.Abstract;
using Logic.Abstract;

namespace Logic.Implementation
{
    internal class LogicBall(Position p) : ILogicBall
    {
        private Position _position = p;

        public Position Position
        {
            get => _position;
            private set
            {
                _position = value;
                NotifyObservers();
            }

        }

        private readonly List<IObserver<ILogicBall>> _observers = [];

        private void NotifyObservers()
        {
            foreach (var observer in _observers.ToArray())
            {
                observer.OnNext(this);
            }
        }

        public void OnCompleted() {}

        public void OnError(Exception error) {}

        public void OnNext(IBall value)
        {
            Position = value.Position;
        }

        public IDisposable Subscribe(IObserver<ILogicBall> observer)
        {
            if (!_observers.Contains(observer))
                _observers.Add(observer);
            return new SubscriptionToken(_observers, observer);
        }


        public class SubscriptionToken : IDisposable
        {
            private readonly ICollection<IObserver<ILogicBall>> _observers;
            private readonly IObserver<ILogicBall> _observer;

            public SubscriptionToken(ICollection<IObserver<ILogicBall>> observers, IObserver<ILogicBall> observer)
            {
                this._observers = observers;
                this._observer = observer;
            }

            public void Dispose()
            {
                if (_observer != null && _observers.Contains(_observer))
                    _observers.Remove(_observer);
            }
        }
    }
}
