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

        public int X { get; set; } = 0;
        public int Y { get; set; } = 0;

        public DirectionVector()
        {
            while (X == 0 && Y == 0)
            {
                X = _rnd.Next(-1, 1);
                Y = _rnd.Next(-1, 1);
            }
        }

        public void Invert()
        {
            X = -X; Y = -Y;
        }
    }
}
