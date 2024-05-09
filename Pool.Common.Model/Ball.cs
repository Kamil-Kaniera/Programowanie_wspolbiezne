namespace Pool.Common.Model;

public class Ball
{
    public Guid BallId { get; set; }
    public Position Position { get; set; }
    public DirectionVector Direction { get; set; }
    public int Radius { get; set; } = Constants.RADIUS;
}