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
            while (X == 0 && Y == 0)
            {
                X = _rnd.Next(-1, 1) * Constants.RESCALE;
                Y = _rnd.Next(-1, 1) * Constants.RESCALE;
            }
        }
    }
}
