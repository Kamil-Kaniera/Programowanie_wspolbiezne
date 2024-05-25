namespace Data
{
    public class VelocityVector
    {
        private readonly Random _rnd = new();
        private const double TwoPi = 2 * Math.PI;

        private const int Rescale = 100;
        private const int Diameter = 20;
        private const int Speed = 2;

        private int _x;
        private int _y;
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

        public VelocityVector()
        {
            var angle = _rnd.NextDouble() * TwoPi;

            while (X == 0 && Y == 0)
            {
                _x = (int)(Math.Cos(angle) * Rescale) * Speed;
                _y = (int)(Math.Sin(angle) * Rescale) * Speed;
            }
        }

        public VelocityVector(int x, int y)
        {
            _x = x switch
            {
                >= 0 and < Diameter => Diameter,
                <= 0 and > -Diameter => -Diameter,
                _ => x
            };

            _y = y switch
            {
                >= 0 and < Diameter => Diameter,
                <= 0 and > -Diameter => -Diameter,
                _ => y
            };
        }
    }
}
