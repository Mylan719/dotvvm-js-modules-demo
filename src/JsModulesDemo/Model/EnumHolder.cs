using System;

namespace JsModulesDemo.Model
{
    public class EnumHolder<T> where T : Enum
    {
        public T Value { get; set; }
        public string Description { get; set; }
        public int IntValue { get; set; }
    }
}