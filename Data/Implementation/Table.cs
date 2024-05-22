using Data.Abstract;

namespace Data.Implementation
{
    public class Table(int x, int y) : ITable
    {
        public int TableX { get; } = x;
        public int TableY { get; } = y;
    }
}
