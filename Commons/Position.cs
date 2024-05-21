using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commons
{
    public readonly struct Position(int x, int y)
    {
        public int X { get; } = x;
        public int Y { get; } = y;
    }
}
