using System;
using UnityEngine;

namespace MVVM
{
    public class CameraTest : MonoBehaviour
    {
        public SetupView SetupView;

        private void Start()
        {
            SetupView.BindeingContext = new SetupViewModel();
        }
    }
}