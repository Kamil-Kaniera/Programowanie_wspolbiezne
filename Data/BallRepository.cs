using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class BallRepository : IBallRepository
    {
        private List<Ball> _balls = [];

        public void AddBall(Ball ball)
        {
            _balls.Add(ball);
            
        }

        public List<Ball> GetBalls()
        {
            return _balls;
        }

        public void RemoveAllBalls()
        {
            _balls.Clear();
        }

        public void RemoveBall(Ball ball)
        {
            _balls.Remove(ball);
        }
    }
}
