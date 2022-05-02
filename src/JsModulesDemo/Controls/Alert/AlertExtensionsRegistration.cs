using DotVVM.Framework.Compilation.Javascript;
using DotVVM.Framework.Compilation.Javascript.Ast;

namespace JsModulesDemo.Controls.Alert
{
    public static class AlertExtensionsRegistration
    {
        public static IServiceCollection AddDotvvmAlertExtensions(this IServiceCollection services)
        {
            services.Configure((Action<JavascriptTranslatorConfiguration>)(c =>
            {
                RegisterAlertMethod(c, nameof(AlertExtensions.ShowSuccess), "showSuccess");
                RegisterAlertMethod(c, nameof(AlertExtensions.ShowInfo), "showInfo");
                RegisterAlertMethod(c, nameof(AlertExtensions.ShowWarning), "showWarning");
                RegisterAlertMethod(c, nameof(AlertExtensions.ShowDanger), "showDanger");
            }));
            return services;
        }

        private static void RegisterAlertMethod(JavascriptTranslatorConfiguration c, string csharpName, string jsName)
        {
            c.MethodCollection.AddMethodTranslator(
                           typeof(AlertExtensions),
                           csharpName,
                           new GenericMethodCompiler((a) =>
                           new JsIdentifierExpression(nameof(AlertExtensions))
                                          .Member(jsName)
                                          .Invoke(
                                                   a[1].WithAnnotation(ShouldBeObservableAnnotation.Instance),
                                                   a[2].WithAnnotation(ShouldBeObservableAnnotation.Instance)
                                               )), 2, allowMultipleMethods: true);
        }
    }
}
