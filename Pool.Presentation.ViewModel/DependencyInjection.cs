using Microsoft.Extensions.DependencyInjection;
using Pool.Data.Abstract;
using Pool.Data.Implementation;
using Pool.Data.Implementation.Mappers;
using Pool.Logic.Abstract;
using Pool.Logic.Implementation;
using Pool.Presentation.Model;

namespace Pool.Presentation.ViewModel;

public class DependencyInjection
{
    static DependencyInjection()
    {
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);
        Provider = serviceCollection.BuildServiceProvider();
    }

    public static IServiceProvider Provider { get; }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<IModelApi, ModelApi>();
        services.AddTransient<ILogicApi, LogicApi>();
        services.AddTransient<IDataApi, DataApi>();
        services.AddTransient<IBallMapper, BallMapper>();
        services.AddTransient<ITableMapper, TableMapper>();
    }
}