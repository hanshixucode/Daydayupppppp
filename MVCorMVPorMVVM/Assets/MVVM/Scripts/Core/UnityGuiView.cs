using UnityEngine;

namespace MVVM
{
    public interface IView<T> where T : ViewModelBase
    { 
        T BindeingContext { get; set; }
        void OnActive();
        void OnShow();
        void OnShowed();
        void OnHide();
        void OnHidden();
        void OnDisappear();
        void OnDestory();
        
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

        /// <summary>
        /// 激活View
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void OnActive()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 显示View
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void OnShow()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 显示后
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void OnShowed()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 隐藏前
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void OnHide()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 隐藏后
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void OnHidden()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 隐藏完成False
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void OnDisappear()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 销毁
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void OnDestory()
        {
            throw new System.NotImplementedException();
        }

        protected virtual void OnBindingContextChanged(T oldViewModel, T newViewModel)
        {
            Binder.Unbind(oldViewModel);
            Binder.Bind(newViewModel);
        }

        protected virtual void OnInit()
        {
            //只触发一次
            this.ViewModelProperty.OnValueChanged += OnBindingContextChanged;
        }
        public UnityGuiView()
        {
            
        }
    }
}