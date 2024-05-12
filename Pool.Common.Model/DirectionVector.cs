using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pool.Common.Model
{
    public class DirectionVector
    {
        private readonly Random _rnd = new();
        private int _x = 0;
        private int _y = 0;
        private const double TwoPi = 2 * Math.PI;

        public int X
        {
            get => _x;
            set
            {
                _x = value switch
                {
                    >= 0 and < Constants.DIAMETER => Constants.DIAMETER,
                    <= 0 and > -Constants.DIAMETER => -Constants.DIAMETER,
                    _ => value
                };
            }
        }

        public int Y
        {
            get => _y;
            set
            {
                _y = value switch
                {
                    >= 0 and < Constants.DIAMETER => Constants.DIAMETER,
                    <= 0 and > -Constants.DIAMETER => -Constants.DIAMETER,
                    _ => value
                };
            }
        }

        public DirectionVector()
        {
            double angle = _rnd.NextDouble() * TwoPi;

            while (X == 0 && Y == 0)
            {
                X = (int)(Math.Cos(angle) * Constants.RESCALE) * Constants.SPEED;
                Y = (int)(Math.Sin(angle) * Constants.RESCALE) * Constants.SPEED;
            }
        }
    }
}
