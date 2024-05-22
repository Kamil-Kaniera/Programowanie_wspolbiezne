using Data.Abstract;

namespace Data.Implementation
{
    public class DataApi : IDataApi
    {
        private const int Rescale = 100;
        private const int TableX = 500;
        private const int TableY = 500;

        public List<IBall> Balls { get; } = [];
        private readonly Table _table = new(TableX * Rescale, TableY * Rescale);


        public IBall AddBall(Position p)
        {
            Ball ball = new Ball(p, new VelocityVector());
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
