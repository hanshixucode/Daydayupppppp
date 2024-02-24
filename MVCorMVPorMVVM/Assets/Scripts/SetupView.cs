using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MVVM
{
    public class SetupView : UnityGuiView
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
        
        protected override void OnBindingContextChanged(ViewModelBase oldViewModel, ViewModelBase newViewModel)
        {
            base.OnBindingContextChanged(oldViewModel, newViewModel);
            SetupViewModel oldVm = oldViewModel as SetupViewModel;
            if (oldVm != null)
            {
                oldVm.Name.OnValueChanged -= NameValueChanged;
            }

            if (ViewModel != null)
            {
                ViewModel.Name.OnValueChanged += NameValueChanged;
            }
        }

        private void NameValueChanged(string oldvalue, string newvalue)
        {
            nameText.text = newvalue.ToString();
        }

        public void NameChanged()
        {
            ViewModel.Name.Value = nameInput.text;
        }
    }
}