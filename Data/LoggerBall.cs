namespace Data
{
    public class LoggerBall
    {
        private readonly int _x;
        private readonly int _y;
        private readonly int _vx;
        private readonly int _vy;
        private readonly Guid _id;
        private readonly DateTime _timestamp;

        public LoggerBall(int x, int y, int vx, int vy, Guid id, DateTime timestamp)
        {
            _x = x;
            _y = y;
            _vx = vx;
            _vy = vy;
            _id = id;
            _timestamp = timestamp;
        }

        public int X => _x;

        public int Y => _y;

        public int Vx => _vx;

        public int Vy => _vy;

        public Guid Id => _id;

        public DateTime Timestamp => _timestamp;
    }
}
