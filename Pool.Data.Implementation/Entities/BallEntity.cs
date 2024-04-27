using Pool.Common.Model;

namespace Pool.Data.Implementation.Entities;

public class BallEntity
{
    public Guid BallId { get; set; }
    public int PositionX { get; set; }
    public int PositionY { get; set; }
    public Direction Direction { get; set; }
}