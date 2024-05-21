using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Abstract;
using Data.Implementation;
using Logic.Abstract;
using Logic.Implementation;
using Model.Abstract;
using Model.Implementation;

namespace ViewModel
{
    public static class DependencyInjection
    {
        private static readonly Dictionary<Type, Func<object>> Services = [];

        static DependencyInjection()
        {
            // Connect abstraction with its implementation
            Register<IDataApi>(() => new DataApi());
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
