using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using MVVM.Extensions;
using MVVM.Message;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MVVM
{
    public class SetupView : UnityGuiView<SetupViewModel>
    {
        public InputField nameInput;
        public Text nameText;
        public InputField jobInput;
        public Text jobText;
        public InputField atkInput;
        public Text atkText;

        public Slider SuccessSlider;
        public Text SuccessSliderText;

        public Toggle JoinToggle;
        public Button joinbtn;
        public Button waitbtn;

        public SubSetupView subSetupView;

        public EventTrigger eventTrigger;
        public SetupViewModel ViewModel
        {
            get
            {
                return (SetupViewModel)BindingContext;
            }
        }

        protected override void OnInit()
        {
            base.OnInit();
            Binder.Add<string>("Name",NameValueChanged);
            Binder.Add<State>("State",BtnValueChanged);
            Binder.Add<Info>("Info",InfoValueChanged);

            var entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback.AddListener((arg0 => { OnClick();}));
            eventTrigger.triggers.Add(entry);
            
        }

        // protected override void OnBindingContextChanged(SetupViewModel oldViewModel, SetupViewModel newViewModel)
        // {
        //     base.OnBindingContextChanged(oldViewModel, newViewModel);
        //     SetupViewModel oldVm = oldViewModel as SetupViewModel;
        //     if (oldVm != null)
        //     {
        //         oldVm.Name.OnValueChanged -= NameValueChanged;
        //         oldVm.State.OnValueChanged -= BtnValueChanged;
        //     }
        //
        //     if (ViewModel != null)
        //     {
        //         ViewModel.Name.OnValueChanged += NameValueChanged;
        //         ViewModel.State.OnValueChanged += BtnValueChanged;
        //     }
        // }

        private void OnClick()
        {
            BindingContext?.OnClick.Invoke();
        }

        private void NameValueChanged(string oldvalue, string newvalue)
        {
            nameText.text = newvalue.ToString();
        }

        private void BtnValueChanged(State oldvalue, State newvalue)
        {
            Debug.Log($"{oldvalue.ToString()} change to {newvalue.ToString()}");
        }
        
        private void InfoValueChanged(Info oldvalue, Info newvalue)
        {
            subSetupView.BindingContext = new SubSetupViewModel(){ ParentViewModel = BindingContext};
            subSetupView.BindingContext.Init(newvalue);
            var parent =  subSetupView.BindingContext.FindParent();
        }

        public async void NameChanged()
        {
            var text = await ChangeAsync();
            ViewModel.Name.Value = nameInput.text + text;
            ViewModel.InitInfo(ViewModel.Name.Value, "coder");
        }

        public async Task<string> ChangeAsync()
        {
            await Task.Delay(2000);
            return "delay2000ms";
        }

        public void Join()
        {
            ViewModel.State.Value = State.yes;
            MessageAggregator<object>.Instance.Publisher("OnbtnClick", this, new MessageArgs<object>("Yes"));
        }

        public void Wait()
        {
            ViewModel.State.Value = State.no;
            MessageAggregator<object>.Instance.Publisher("OnbtnClick", this, new MessageArgs<object>("No"));
        }
    }
}