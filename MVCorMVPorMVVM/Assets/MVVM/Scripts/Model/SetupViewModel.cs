﻿namespace MVVM
{
    public class SetupViewModel : ViewModelBase
    {
        public BindableProperty<string> Name = new BindableProperty<string>();
        public BindableProperty<string> Job = new BindableProperty<string>();
        public BindableProperty<int> ATK = new BindableProperty<int>();
        public BindableProperty<float> SuccessRate = new BindableProperty<float>();
        public BindableProperty<State> State = new BindableProperty<State>();
    }
    
    /// <summary>
    /// 加入or不加入
    /// </summary>
    public enum State
    {
        yes,
        no
    }
}