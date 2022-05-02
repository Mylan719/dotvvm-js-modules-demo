namespace Infrastructure.DotVVM.Models
{
    public class ControlProperty<T>
    {
        public ControlProperty(T value)
        {
            Value = value;
        }
        public T Value { get; set; }
    }
}
