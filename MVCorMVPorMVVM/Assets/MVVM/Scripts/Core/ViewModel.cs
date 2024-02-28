namespace MVVM
{
    /// <summary>
    /// viewModel的基类
    /// </summary>
    public class ViewModelBase
    {
        private bool isInit;
        protected bool IsShowed { get; set; }
        protected bool IsShowing { get; set; }
        protected bool IsHide { get; set; }
        protected bool IsHideing { get; set; }
        /// <summary>
        /// model初始化
        /// </summary>
        protected virtual void OnInit()
        {
            
        }

        public virtual void OnStartShow()
        {
            IsShowing = true;
            if (!isInit)
            {
                isInit = true;
                OnInit();
            }
        }
        
        public virtual void OnFinishShow()
        {
            IsShowing = false;
            IsShowed = true;
        }
        
        public virtual void OnStartHide()
        {
            IsHideing = true;
        }
        
        public virtual void OnFinishHide()
        {
            IsHideing = false;
            IsShowed = false;
            IsHide = true;
        }

        public virtual void OnDestory()
        {
            
        }
    }
}