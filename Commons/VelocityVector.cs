using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commons
{
    public class VelocityVector
    {
        private readonly Random _rnd = new();
        private const double TwoPi = 2 * Math.PI;

        public int X { get; }

        public int Y { get; }

        public VelocityVector()
        {
            var angle = _rnd.NextDouble() * TwoPi;

            while (X == 0 && Y == 0)
            {
                X = (int)(Math.Cos(angle) * Constants.RESCALE) * Constants.SPEED;
                Y = (int)(Math.Sin(angle) * Constants.RESCALE) * Constants.SPEED;
            }
        }

        public VelocityVector(int x, int y)
        {
            X = x switch
            {
                >= 0 and < Constants.DIAMETER => Constants.DIAMETER,
                <= 0 and > -Constants.DIAMETER => -Constants.DIAMETER,
                _ => x
            };

            Y = y switch
            {
                >= 0 and < Constants.DIAMETER => Constants.DIAMETER,
                <= 0 and > -Constants.DIAMETER => -Constants.DIAMETER,
                _ => y
            };
        }
    }
}
