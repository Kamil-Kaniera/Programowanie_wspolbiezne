using Pool.Data.Abstract;
using Pool.Data.Implementation.Mappers;
using Pool.Data.Implementation;
using Pool.Logic.Abstract;
using Pool.Logic.Implementation;
using Pool.Presentation.Model;

namespace Pool.Presentation.ViewModel
{
    public static class DependencyInjection
    {
        private static readonly Dictionary<Type, Func<object>> Services = [];

        static DependencyInjection()
        {
            // Connect abstraction with its implementation
            Register<IBallMapper>(() => new BallMapper());
            Register<ITableMapper>(() => new TableMapper());
            Register<IDataApi>(() => new DataApi(Get<IBallMapper>(), Get<ITableMapper>()));
            Register<ILogicApi>(() => new LogicApi(Get<IDataApi>()));
            Register<IModelApi>(() => new ModelApi(Get<ILogicApi>()));
        }

        public static void Register<T>(Func<object> implementationFactory)
        {
            Services[typeof(T)] = implementationFactory;
        }

        public static T Get<T>()
        {
            if (Services.TryGetValue(typeof(T), out var implementationFactory))
            {
                return (T)implementationFactory();
            }

            throw new InvalidOperationException($"No implementation registered for type {typeof(T).FullName}");
        }

    }
}
