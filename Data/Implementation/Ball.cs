using System.Diagnostics;
using Data.Abstract;
using System.Collections.Concurrent;
namespace Data.Implementation
{
    public class Ball : IBall
    {
        public Position Position { get; private set; }
        public VelocityVector Velocity { get;  set; }
        public Guid BallId { get; }

        private bool _movement = false;
        private Thread _thread;

        private const int MoveIntervalMs = 4;
        private readonly Stopwatch _stopwatch = new();

        private bool _disposed = false;

        private readonly List<IObserver<IBall>> _observers = [];

        private DataLogger _dataLogger;
        public Ball(Position p, VelocityVector v, DataLogger logger)
        {
            Position = p;
            Velocity = v;
            BallId = Guid.NewGuid();
            _movement = true;
            _thread = new Thread(Run);
            _thread.Start();
            _dataLogger = logger;
        }
        
        private void Run()
        {
            var previousTime = 0;

            _stopwatch.Start();

            while (_movement)
            {
                var currentTime = (int)_stopwatch.ElapsedMilliseconds;

                var moveTime = currentTime - previousTime;

                MoveSelf(moveTime);
                LoggBall();

                previousTime = currentTime;

                var waitingPeriod = 0;

                // Set the waitingPeriod so that all tasks have the same delay 
                if (MoveIntervalMs - moveTime > 0)
                    waitingPeriod = MoveIntervalMs - moveTime;

                Thread.Sleep(waitingPeriod);

            }
        }

        public void LoggBall()
        {
            string timestamp = DateTime.Now.ToString("HH:mm:ss.fff");
            _dataLogger.AddBall(new LoggerBall(Position.X, Position.Y, Velocity.X, Velocity.Y, BallId,timestamp));

        }
        private void MoveSelf(int time)
        {
            Position = new(Position.X + (Velocity.X * time), Position.Y + (Velocity.Y * time));
            NotifyObservers();
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
