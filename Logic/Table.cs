using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class Table(int sizeX, int sizeY)
    {
        private readonly int _sizeX = sizeX;
        private readonly int _sizeY = sizeY;

        public int SizeX { get { return _sizeX; } }
        public int SizeY { get { return _sizeY; } }
    }
}
