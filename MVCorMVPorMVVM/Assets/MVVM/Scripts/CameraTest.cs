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
            SetupView.BindeingContext = new SetupViewModel();
            MessageAggregator<object>.Instance.Sublisher("OnbtnClick", (sender, args) =>
            {
                Debug.Log(args._info);
            });
        }
    }
}