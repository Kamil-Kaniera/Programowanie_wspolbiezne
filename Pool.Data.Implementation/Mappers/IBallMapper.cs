using Pool.Common.Model;
using Pool.Data.Implementation.Entities;

namespace Pool.Data.Implementation.Mappers;

public interface IBallMapper
{
    Ball Map(BallEntity ball);
    BallEntity Map(Ball ball);
}