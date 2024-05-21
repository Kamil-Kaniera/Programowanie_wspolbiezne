using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Commons;

namespace Data.Abstract
{
    internal interface ITable
    {
        public TableSize TableSize { get; }
    }
}
