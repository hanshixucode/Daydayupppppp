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
    }

    public class Info
    {
        public string name { get; set; }
        public string job { get; set; }
    }
}