using Commons;
using Data.Abstract;

namespace Data.Implementation
{
    public class DataApi : IDataApi
    {
        public List<IBall> Balls { get; } = [];
        private readonly Table _table = new(new TableSize(Constants.TABLE_X * Constants.RESCALE, Constants.TABLE_Y * Constants.RESCALE));


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
