using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace Logic
{
    public class BallService(Table table) : IBallService
    {
        private readonly Table _table = table;
        private BallRepository repository = new();

        public void createBalls(int numberOfBalls)
        {
            for (int i = 0; i < numberOfBalls; i++)
            {
                Random rnd = new();
                int randomizedX = rnd.Next(0, table.Width);
                int randomizedY = rnd.Next(0, table.Length);

                repository.addBall(new(randomizedX, randomizedY));
            }

        }

        public void moveBall(Ball ball, int x, int y)
        {
            ball.positionX += x;
            ball.positionY += y;
        }

        public List<Ball> getBalls()
        {
            return repository.getBalls();
        }
    }
}
