using System.Diagnostics;
using Data.Abstract;

namespace Data.Implementation
{
    public class Ball : IBall
    {
        public Position Position { get; private set; }
        public VelocityVector Velocity { get;  set; }

        private bool _movement = false;
        private Thread _thread;

        private const int MoveIntervalMs = 2;
        private readonly Stopwatch _stopwatch = new();

        private bool _disposed = false;

        private readonly List<IObserver<IBall>> _observers = [];

        public Ball(Position p, VelocityVector v)
        {
            Position = p;
            Velocity = v;
            _movement = true;
            _thread = new Thread(Run);
            _thread.Start();
        }
        
        private void Run()
        {
            while (_movement)
            {

                // Restart the stopwatch
                _stopwatch.Restart();

                MoveSelf(MoveIntervalMs / 2);

                // Stop the stopwatch and measure time of MoveSelf()
                _stopwatch.Stop();

                var waitingPeriod = 0;

                // Set the waitingPeriod so that all tasks have the same delay 
                if (MoveIntervalMs - _stopwatch.ElapsedMilliseconds > 0)
                    waitingPeriod = MoveIntervalMs - (int)_stopwatch.ElapsedMilliseconds;

                Thread.Sleep(waitingPeriod);

            }
        }

        private void MoveSelf(int time)
        {
           // lock (this)
            {
                Position = new(Position.X + (Velocity.X * time), Position.Y + (Velocity.Y * time));
                NotifyObservers();
            }
          
        }

        private void NotifyObservers()
        {
            foreach (var observer in _observers.ToArray())
            {
                observer.OnNext(this);
            }
        }

        public IDisposable Subscribe(IObserver<IBall> observer)
        {
            if (!_observers.Contains(observer))
                _observers.Add(observer);
            return new SubscriptionToken(_observers, observer);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                _movement = false;
                _thread.Join(); // Ensure the thread finishes
                _stopwatch.Stop();
            }
            _disposed = true;
        }

        ~Ball()
        {
            Dispose(false);
        }

        public class SubscriptionToken : IDisposable
        {
            private readonly ICollection<IObserver<IBall>> _observers;
            private readonly IObserver<IBall> _observer;

            public SubscriptionToken(ICollection<IObserver<IBall>> observers, IObserver<IBall> observer)
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
