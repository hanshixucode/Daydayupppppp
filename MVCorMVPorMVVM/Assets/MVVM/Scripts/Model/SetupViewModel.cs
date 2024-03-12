using System;
using System.Collections.Generic;
using MVVM.Extensions;
using UnityEditor;

namespace MVVM
{
    public class SetupViewModel : ViewModelBase
    {
        public BindableProperty<string> Name = new BindableProperty<string>();
        public BindableProperty<string> Job = new BindableProperty<string>();
        public BindableProperty<int> ATK = new BindableProperty<int>();
        public BindableProperty<float> SuccessRate = new BindableProperty<float>();
        public BindableProperty<State> State = new BindableProperty<State>();
        public BindableProperty<Info> Info = new BindableProperty<Info>();

        public delegate void OnClickHandler();
        public OnClickHandler OnClick;
        
        public void InitInfo(string name, string job)
        {
            Info.Value = new Info() { name = name, job = job };
        }

        public SetupViewModel FindParent()
        {
            this.Test();
            try
            {
                return this.Ancestors<SetupViewModel>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
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