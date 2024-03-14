using System;
using DG.Tweening;
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
        void OnShow(bool immediate);
        void OnShowed();
        void OnHide(bool immediate);
        void OnHidden();
        void OnDisappear();
        void OnDestory();
    }
    public class UnityGuiView<T>: MonoBehaviour, IDisposable, IView<T> where T : ViewModelBase
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
        /// 显示之后的回掉函数
        /// </summary>
        public Action ShowedAction { get; set; }
        /// <summary>
        /// 隐藏之后的回掉函数
        /// </summary>
        public Action HiddenAction { get; set; }


        public void Show(bool immediate, Action call)
        {
            if (call != null)
            {
                ShowedAction += call;
            }
            OnActive();
            OnShow(immediate);
            OnShowed();
        }
        
        public void Hide(bool immediate, Action call)
        {
            if (call != null)
            {
                HiddenAction -= call;
            }
            OnHide(immediate);
            OnHidden();
            OnDisappear();
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
        public virtual void OnShow(bool immediate)
        {
            if (immediate)
            {
                transform.localScale = Vector3.one;
                GetComponent<CanvasGroup>().alpha = 1;
            }
            else
            {
                //延迟显示的动效之类
                StartAnimatedReveal();
            }
        }

        /// <summary>
        /// 显示后
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public virtual void OnShowed()
        {
            BindingContext.OnFinishShow();
            //显示后的回调函数
            ShowedAction?.Invoke();
        }

        /// <summary>
        /// 隐藏前
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public virtual void OnHide(bool immediate)
        {
            if (immediate)
            {
                transform.localScale = Vector3.zero;
            }
            else
            {
                //延迟消失的动效之类
                StartAnimatedHide();
            }
        }

        /// <summary>
        /// 隐藏后
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public virtual void OnHidden()
        {
            HiddenAction?.Invoke();
            //显示后的回调函数
        }

        /// <summary>
        /// 隐藏完成False
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public virtual void OnDisappear()
        {
            gameObject.SetActive(false);
            BindingContext.OnFinishHide();
        }

        /// <summary>
        /// 销毁
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public virtual void OnDestory()
        {
            BindingContext.OnDestory();
            BindingContext = null;
            ViewModelProperty.OnValueChanged = null;
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
        /// <summary>
        /// scale:1,alpha:1
        /// </summary>
        protected virtual void StartAnimatedReveal()
        {
            var canvasGroup = GetComponent<CanvasGroup>();
            canvasGroup.interactable = false;
            transform.localScale = Vector3.one;

            canvasGroup.DOFade(1, 0.2f).SetDelay(0.2f).OnComplete(() =>
            {
                canvasGroup.interactable = true;
            });
        }
        /// <summary>
        /// alpha:0,scale:0
        /// </summary>
        protected virtual void StartAnimatedHide()
        {
            var canvasGroup = GetComponent<CanvasGroup>();
            canvasGroup.interactable = false;
            canvasGroup.DOFade(0, 0.2f).SetDelay(0.5f).OnComplete(() =>
            {
                transform.localScale = Vector3.zero;
                canvasGroup.interactable = true;
            });
        }

        public virtual void Dispose()
        {
            Console.WriteLine("something dispose");
        }
    }
}