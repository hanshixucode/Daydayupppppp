namespace MVVM
{
    public class BindableProperty<T>
    {
        public delegate void ValueChangedHandler(T oldValye, T newValue);

        public ValueChangedHandler OnValueChanged;

        private T _value;

        public T Value
        {
            get { return _value; }
            set
            {
                if (!object.Equals(_value, value))
                {
                    T old = _value;
                    _value = value;
                    ValueChanged(old, _value);
                }
            }
        }

        private void ValueChanged(T oldValue, T newValue)
        {
            OnValueChanged?.Invoke(oldValue, newValue);
        }

        public override string ToString()
        {
            return (Value != null ? Value.ToString() : "null");
        }
    }
}