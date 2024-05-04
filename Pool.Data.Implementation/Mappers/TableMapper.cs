using Pool.Common.Model;
using Pool.Data.Implementation.Entities;

namespace Pool.Data.Implementation.Mappers;

public class TableMapper : ITableMapper
{
    public Table Map(TableEntity table)
    {
        return new() { Size = new() { X = table.SizeX, Y = table.SizeY } };
    }

    public TableEntity Map(Table table)
    {
        return new() { SizeX = table.Size.X, SizeY = table.Size.Y };
    }
}