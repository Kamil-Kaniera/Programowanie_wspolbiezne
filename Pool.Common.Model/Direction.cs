namespace Pool.Common.Model;

public enum Direction
{
    Up = 1,
    Right = 2,
    Down = 3,
    Left = 4
}

public static class DirectionExtensions
{
    public static Direction Invert(this Direction direction)
    {
        return direction switch
        {
            Direction.Up => Direction.Down,
            Direction.Left => Direction.Right,
            Direction.Right => Direction.Left,
            Direction.Down => Direction.Up
        };
    }
}