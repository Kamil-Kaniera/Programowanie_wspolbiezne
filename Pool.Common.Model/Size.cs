namespace Pool.Common.Model;

public struct Size(int x, int y)
{
    public int X { get; set; } = x;
    public int Y { get; set; } = y;
}