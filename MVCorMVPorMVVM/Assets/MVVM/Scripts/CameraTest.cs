using System;
using MVVM.Message;
using UnityEngine;

namespace MVVM
{
    public class CameraTest : MonoBehaviour
    {
        public SetupView SetupView;

        private void Start()
        {
            SetupView.BindingContext = new SetupViewModel();
            SetupView.BindingContext.OnClick += () =>
            {
                Debug.Log("我点击了按钮");
            };
            MessageAggregator<object>.Instance.Sublisher("OnbtnClick", (sender, args) =>
            {
                Debug.Log(args._info);
            });
            SetupView.Show(false, () =>
            {
                Debug.Log("showed");
            });
        }
    }
}