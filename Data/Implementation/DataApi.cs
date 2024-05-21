using Commons;
using Data.Abstract;

namespace Data.Implementation
{
    public class DataApi : IDataApi
    {
        public List<IBall> Balls { get; } = [];
        private readonly Table _table = new(new TableSize(Constants.TABLE_X * Constants.RESCALE, Constants.TABLE_Y * Constants.RESCALE));


        public void AddBall(Ball ball)
        {
            Balls.Add(ball);
        }

        public void RemoveAllBalls()
        {
            Balls.Clear();
        }

        public Table GetTable()
        {
            return _table;
        }
    }
}
