using System;
using System.Collections.Generic;
using MVVM.Extensions;

namespace MVVM
{
    public class SubSetupViewModel : ViewModelBase
    {
        public BindableProperty<string> name = new BindableProperty<string>();
        public BindableProperty<string> job = new BindableProperty<string>();

        public void Init(Info info)
        {
            name.Value = info.name;
            job.Value = info.job;
        }
        
        public SetupViewModel FindParent()
        {
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

    public class Info
    {
        public string name { get; set; }
        public string job { get; set; }
    }
}