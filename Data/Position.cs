namespace Data
{
    public class Position(int x, int y)
    {
        private int _x = x;
        private int _y = y;

        private readonly object _lock = new();

        public int X
        {
            get
            {
                lock (_lock)
                {
                    return _x;
                }
            }

            private set
            {
                lock (_lock)
                {
                    _x = value;
                }
            }
        }

        public int Y
        {
            get
            {
                lock (_lock)
                {
                    return _y;
                }
            }

            private set
            {
                lock (_lock)
                {
                    _y = value;
                }
            }
        }
    }
}
