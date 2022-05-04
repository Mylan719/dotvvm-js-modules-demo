using System.Collections.Generic;

namespace JsModulesDemo.Model
{
    public record UserInfoModel
    {
        public List<string> Names { get; set; } = new List<string>();
        public int Count { get; set; }
    }
}