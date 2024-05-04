using Pool.Common.Model;
using Pool.Data.Implementation.Entities;

namespace Pool.Data.Implementation.Mappers;

public interface ITableMapper
{
    Table Map(TableEntity table);
    TableEntity Map(Table table);
}