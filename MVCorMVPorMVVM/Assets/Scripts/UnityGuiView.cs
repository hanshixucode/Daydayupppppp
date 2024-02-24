using UnityEngine;

namespace MVVM
{
    public interface IView
    {
        ViewModelBase BindeingContext { get; set; }
    }
    public class UnityGuiView: MonoBehaviour,IView
    {
        public readonly BindableProperty<ViewModelBase> ViewModelProperty = new BindableProperty<ViewModelBase>();

        public ViewModelBase BindeingContext
        {
            get
            {
                return ViewModelProperty.Value;
            }
            set
            {
                ViewModelProperty.Value = value;
            }
        }
        
        protected virtual void OnBindingContextChanged(ViewModelBase oldViewModel, ViewModelBase newViewModel)
        {
        }

        public UnityGuiView()
        {
            this.ViewModelProperty.OnValueChanged += OnBindingContextChanged;
        }
    }
}