﻿using Pool.Common.Model;
using Pool.Data.Abstract;
using Pool.Data.Implementation.Entities;
using Pool.Data.Implementation.Mappers;

namespace Pool.Data.Implementation;

public class DataApi(IBallMapper ballMapper, ITableMapper tableMapper) : IDataApi
{
    private readonly List<BallEntity> _balls = [];
    private readonly TableEntity _table = new() { SizeX = 450, SizeY = 450 };
    public IEnumerable<Ball> Balls => _balls.Select(ballMapper.Map);

    public void AddBall(Ball ball)
    {
        _balls.Add(ballMapper.Map(ball));
    }

    public void RemoveAllBalls()
    {
        _balls.Clear();
    }

    public void UpdateBall(Ball ball)
    {
        var entity = _balls.First(b => b.BallId == ball.BallId);
        entity.PositionX = ball.Position.X;
        entity.PositionY = ball.Position.Y;
        entity.Direction = ball.Direction;
    }

    public Table GetTable()
    {
        return tableMapper.Map(_table);
    }

    public Ball GetBall(Guid ballId)
    {
        return ballMapper.Map(_balls.First(b => b.BallId == ballId));
    }
}