using UnityEngine.UI;

namespace MVVM
{
    public class SubSetupView : UnityGuiView<SubSetupViewModel>
    {
        public Text name;
        public Text job;
        
        public SubSetupViewModel ViewModel
        {
            get
            {
                return (SubSetupViewModel)BindingContext;
            }
        }

        protected override void OnInit()
        {
            base.OnInit();
            Binder.Add<string>("name", OnNameValueChanged);
            Binder.Add<string>("job", OnjobValueChanged);
        }
        
        private void OnjobValueChanged(string oldValue, string newValue)
        {
            name.text = newValue;
        }

        private void OnNameValueChanged(string oldValue, string newValue)
        {
            job.text = newValue;
        }
    }
}