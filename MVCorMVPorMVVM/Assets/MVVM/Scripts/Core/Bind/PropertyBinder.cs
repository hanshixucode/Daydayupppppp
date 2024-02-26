using System.Collections.Generic;
using System.Reflection;

namespace MVVM
{
    /// <summary>
    /// 属性绑定器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PropertyBinder<T> where T : ViewModelBase
    {
        private delegate void BindHandler(T viewModel);
        private delegate void UnBindHandler(T viewModel);

        private readonly List<BindHandler> _bindHandlers = new List<BindHandler>();
        private readonly List<UnBindHandler> _unbindHandlers = new List<UnBindHandler>();

        public void Add<TProperty>(string name, BindableProperty<TProperty>.ValueChangedHandler valueChangedHandler)
        {
            var fieldInfo = typeof(T).GetField(name, BindingFlags.Instance | BindingFlags.Public);
            if (fieldInfo == null)
            {
                return;
            }

            _bindHandlers.Add((model =>
            {
                GAetPropertyValue<TProperty>(name, model, fieldInfo).OnValueChanged += valueChangedHandler;
            }));
            
            _unbindHandlers.Add((model =>
            {
                GAetPropertyValue<TProperty>(name, model, fieldInfo).OnValueChanged -= valueChangedHandler;
            }));
            
        }

        private BindableProperty<TProperty> GAetPropertyValue<TProperty>(string name, T viewModel, FieldInfo fieldInfo)
        {
            var bindableProperty = fieldInfo.GetValue(viewModel) as BindableProperty<TProperty>;
            if (bindableProperty == null)
            {
                return null;
            }

            return bindableProperty;
        }
        
        public void Bind(T viewmodel)
        {
            if (viewmodel!=null)
            {
                for (int i = 0; i < _bindHandlers.Count; i++)
                {
                    _bindHandlers[i](viewmodel);
                }
            }
        }

        public void Unbind(T viewmodel)
        {
            if (viewmodel!=null)
            {
                for (int i = 0; i < _unbindHandlers.Count; i++)
                {
                    _unbindHandlers[i](viewmodel);
                }
            }
        }
    }
}