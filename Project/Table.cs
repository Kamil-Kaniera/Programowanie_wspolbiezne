using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class Table(int length, int width)
    {
        private readonly int _length = length;
        private readonly int _width = width;

        public int Length { get { return _length; } }
        public int Width { get { return _width; } }
    }
}
