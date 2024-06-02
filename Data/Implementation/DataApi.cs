using Data.Abstract;

namespace Data.Implementation
{
    public class DataApi : IDataApi
    {
        private const int Rescale = 100;
        private const int TableX = 500;
        private const int TableY = 500;

        public DataApi()
        {
            _dataLogger = new DataLogger();
        }

        public List<IBall> Balls { get; } = [];
        private readonly Table _table = new(TableX * Rescale, TableY * Rescale);
        private DataLogger _dataLogger;

        public IBall AddBall(Position p)
        {
            Ball ball = new Ball(p, new VelocityVector(), _dataLogger);
            Balls.Add(ball);
            return ball;
        }

        public void RemoveAllBalls()
        {
            foreach (var ball in Balls )
            {
                ball.Dispose();
            }
            Balls.Clear();
        }

        public ITable GetTable()
        {
            return _table;
        }
    }
}
