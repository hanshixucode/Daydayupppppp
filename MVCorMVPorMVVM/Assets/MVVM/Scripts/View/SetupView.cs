using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

        public SetupViewModel ViewModel
        {
            get
            {
                return (SetupViewModel)BindeingContext;
            }
        }

        protected override void OnInit()
        {
            base.OnInit();
            Binder.Add<string>("Name",NameValueChanged);
            Binder.Add<State>("State",BtnValueChanged);
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

        private void NameValueChanged(string oldvalue, string newvalue)
        {
            nameText.text = newvalue.ToString();
        }

        private void BtnValueChanged(State oldvalue, State newvalue)
        {
            Debug.Log($"{oldvalue.ToString()} change to {newvalue.ToString()}");
        }

        public void NameChanged()
        {
            ViewModel.Name.Value = nameInput.text;
        }

        public void Join()
        {
            ViewModel.State.Value = State.yes;
        }

        public void Wait()
        {
            ViewModel.State.Value = State.no;
        }
    }
}