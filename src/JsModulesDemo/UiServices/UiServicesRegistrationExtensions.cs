using Infrastructure.DotVVM.UiServices;
using System.Reflection;

namespace JsModulesDemo.UiServices
{
    public static class UiServicesRegistrationExtensions
    {
        public static IServiceCollection AddUiServices(this IServiceCollection services)
        {
            services.Scan(scan =>
               scan.FromAssemblies(typeof(UiServicesRegistrationExtensions).Assembly)
               .AddClasses(s => s.AssignableTo<IUiService>())
               .AsSelf()
               .AsImplementedInterfaces());

            return services;
        }
    }
}
