using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JsModulesDemo.BusinessServices
{
    public static class BusinessServiceCollectionExtensions
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            services.Scan(scan =>
              scan.FromAssembliesOf(typeof(BusinessServiceCollectionExtensions))
              .AddClasses(s => s.AssignableTo<IService>())
              .AsSelf()
              .AsImplementedInterfaces());

            return services;
        }
    }
}
