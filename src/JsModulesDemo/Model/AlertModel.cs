using Infrastructure.DotVVM.Models;
using JsModulesDemo.Model.Enums;

namespace JsModulesDemo.Model
{
    public class AlertModel
    {
        public string Message { get; set; }
        public AlertType Type { get; set; }
        public ControlProperty<bool> IsVisible { get; set; } = new ControlProperty<bool>(false);
    }
}