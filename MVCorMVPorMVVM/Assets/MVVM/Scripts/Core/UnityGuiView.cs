using UnityEngine;

namespace MVVM
{
    /// <summary>
    /// View基础接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IView<T> where T : ViewModelBase
    { 
        T BindingContext { get; set; }
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
        public T BindingContext
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
        public virtual void OnActive()
        {
            gameObject.SetActive(true);
            BindingContext.OnStartShow();
        }

        /// <summary>
        /// 显示View
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public virtual void OnShow()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 显示后
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public virtual void OnShowed()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 隐藏前
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public virtual void OnHide()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 隐藏后
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public virtual void OnHidden()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 隐藏完成False
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public virtual void OnDisappear()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 销毁
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public virtual void OnDestory()
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