using DotVVM.Framework.Hosting;
using DotVVM.Framework.ViewModel;
using Infrastructure.DotVVM.UiServices;
using JsIntegrationDemo.Hubs;
using JsModulesDemo.BusinessServices;
using JsModulesDemo.Model;
using Microsoft.AspNetCore.SignalR;

namespace JsModulesDemo.Pages.Default
{
    public class DefaultUiService : UiServiceBase
    {
        public DefaultUiService(IDotvvmRequestContext dotvvmRequestContext)
            : base(dotvvmRequestContext)
        {
        }

        [AllowStaticCommand]
        public void CreateDashboard()
        {
            Context.RedirectToRoute("Dashboard", new { dashboardId = Guid.NewGuid() });
        }
    }
}
