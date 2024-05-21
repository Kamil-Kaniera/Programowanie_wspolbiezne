using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Commons;
using Data.Abstract;

namespace Data.Implementation
{
    public class Table(TableSize size) : ITable
    {
        public TableSize TableSize { get; } = size;
    }
}
