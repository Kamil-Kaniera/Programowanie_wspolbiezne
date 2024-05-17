namespace Pool.Common.Model;

public class Ball
{
    public Guid BallId { get; set; }
    public Position Position { get; set; }
    public DirectionVector Direction { get; set; }
    public int Diameter { get; set; } = Constants.DIAMETER * Constants.RESCALE;

    public void MoveSelf()
    {
        Position = new(Position.X + Direction.X, Position.Y + Direction.Y);
    }

    public void HandleBallCollision(Ball otherBall)
    {
        double normalX = otherBall.Position.X - Position.X;
        double normalY = otherBall.Position.Y - Position.Y;
        double normalLength = Math.Sqrt((normalX * normalX + normalY * normalY));
        normalX /= normalLength;
        normalY /= normalLength;

        double dotProduct1 = Direction.X * normalX + Direction.Y * normalY;
        double dotProduct2 = otherBall.Direction.X * normalX + otherBall.Direction.Y * normalY;

        double newVx1 = Direction.X - 2 * dotProduct1 * normalX;
        double newVy1 = Direction.Y - 2 * dotProduct1 * normalY;
        double newVx2 = otherBall.Direction.X - 2 * dotProduct2 * normalX;
        double newVy2 = otherBall.Direction.Y - 2 * dotProduct2 * normalY;

        Direction = new((int)newVx1, (int)newVy1);
        otherBall.Direction = new((int)newVx2, (int)newVy2);
    }

    public void HandleWallCollision(Table table)
    {
        if (Position.X <= 0)
            Direction = new(Math.Abs(Direction.X), Direction.Y);
        if (Position.Y <= 0)
            Direction = new(Direction.X, Math.Abs(Direction.Y));

        if (Position.X >= table.Size.X - Diameter)
            Direction = new(-Math.Abs(Direction.X), Direction.Y);
        if (Position.Y >= table.Size.Y - Diameter)
            Direction = new(Direction.X, -Math.Abs(Direction.Y));
    }
}