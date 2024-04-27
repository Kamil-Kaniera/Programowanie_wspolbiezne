namespace Pool.Common.Model;

public class Ball
{
    public Guid BallId { get; set; }
    public Position Position { get; set; }
    public Direction Direction { get; set; }
}