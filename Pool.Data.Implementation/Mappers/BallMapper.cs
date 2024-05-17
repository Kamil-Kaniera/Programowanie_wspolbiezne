using Pool.Common.Model;
using Pool.Data.Implementation.Entities;

namespace Pool.Data.Implementation.Mappers;

public class BallMapper : IBallMapper
{
    public Ball Map(BallEntity ball)
    {
        return new()
        {
            BallId = ball.BallId,
            Position = new(ball.PositionX, ball.PositionY),
            Direction = new(ball.DirectionX, ball.DirectionY),
            Diameter = ball.Diameter
        };
    }

    public BallEntity Map(Ball ball)
    {
        return new()
        {
            BallId = ball.BallId,
            PositionX = ball.Position.X,
            PositionY = ball.Position.Y,
            DirectionX = ball.Direction.X,
            DirectionY = ball.Direction.Y,
            Diameter = ball.Diameter
        };
    }
}