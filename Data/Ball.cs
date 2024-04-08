using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Ball(int x, int y)
    {
        private int _X = x;
        private int _Y = y;

        public int positionX { get { return _X; } set { _X = value; } }
        public int positionY { get { return _Y; } set { _Y = value; } }
    }
}
