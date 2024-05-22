namespace Data
{
    public class VelocityVector
    {
        private readonly Random _rnd = new();
        private const double TwoPi = 2 * Math.PI;

        private const int Rescale = 100;
        private const int Diameter = 20;
        private const int Speed = 2;

        public int X { get; }

        public int Y { get; }

        public VelocityVector()
        {
            var angle = _rnd.NextDouble() * TwoPi;

            while (X == 0 && Y == 0)
            {
                X = (int)(Math.Cos(angle) * Rescale) * Speed;
                Y = (int)(Math.Sin(angle) * Rescale) * Speed;
            }
        }

        public VelocityVector(int x, int y)
        {
            X = x switch
            {
                >= 0 and < Diameter => Diameter,
                <= 0 and > -Diameter => -Diameter,
                _ => x
            };

            Y = y switch
            {
                >= 0 and < Diameter => Diameter,
                <= 0 and > -Diameter => -Diameter,
                _ => y
            };
        }
    }
}
