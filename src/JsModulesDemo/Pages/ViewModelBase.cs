using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.ViewModel;

namespace JsModulesDemo.Pages
{
    public class ViewModelBase : DotvvmViewModelBase
    {
        protected CancellationToken RequestCancellationToken => Context.GetAspNetCoreContext().RequestAborted;

        public override async Task Init()
        {
            var routeName = Context.Route?.RouteName;
            if (routeName != null)
            {
                Context.ResourceManager.TryAddRequiredResource($"{routeName}-css");
                Context.ResourceManager.TryAddRequiredResource($"{routeName}-js");
            }

            await base.Init();
        }

        public static GridViewDataSet<TModel> InitGrid<TModel>(bool longList = false)
            => new GridViewDataSet<TModel>
            {
                PagingOptions = new PagingOptions
                {
                    PageSize = longList ? 15 : 6,
                }
            };
    }
}
