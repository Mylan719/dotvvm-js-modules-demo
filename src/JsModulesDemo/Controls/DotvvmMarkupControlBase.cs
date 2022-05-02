using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using JsModulesDemo;

namespace JsModulesDemo.Controls
{
    public class DotvvmMarkupControlBase : DotvvmMarkupControl
    {
        protected override void OnInit(IDotvvmRequestContext context)
        {
            var controlName = GetType().Name;
            context.ResourceManager.TryAddRequiredResource($"{controlName}-css");
            context.ResourceManager.TryAddRequiredResource($"{controlName}-js");
            base.OnInit(context);
        }
    }
}