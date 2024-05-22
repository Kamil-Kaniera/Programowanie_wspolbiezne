using Commons;
using Data.Abstract;

namespace Data.Implementation
{
    public class Table(TableSize size) : ITable
    {
        public TableSize TableSize { get; } = size;
    }
}
