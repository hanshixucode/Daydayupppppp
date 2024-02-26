using UnityEngine;

namespace MVVM
{
    public interface IView<T> where T : ViewModelBase
    { 
        T BindeingContext { get; set; }
    }
    public class UnityGuiView<T>: MonoBehaviour,IView<T> where T : ViewModelBase
    {
        private bool isInit;
        public readonly BindableProperty<T> ViewModelProperty = new BindableProperty<T>();
        public readonly PropertyBinder<T> Binder = new PropertyBinder<T>();
        public T BindeingContext
        {
            get
            {
                return ViewModelProperty.Value;
            }
            set
            {
                if (!isInit)
                {
                    OnInit();
                    isInit = false;
                }
                ViewModelProperty.Value = value;
            }
        }
        
        protected virtual void OnBindingContextChanged(T oldViewModel, T newViewModel)
        {
            Binder.Unbind(oldViewModel);
            Binder.Bind(newViewModel);
        }

        protected virtual void OnInit()
        {
            this.ViewModelProperty.OnValueChanged += OnBindingContextChanged;
        }
        public UnityGuiView()
        {
            
        }
    }
}