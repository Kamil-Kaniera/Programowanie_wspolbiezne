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

        public void addBall(Ball ball)
        {
            _balls.Add(ball);
        }

        public List<Ball> getBalls()
        {
            return _balls;
        }

        public void removeBall(Ball ball)
        {
            _balls.Remove(ball);
        }
    }
}
