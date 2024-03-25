using System;
using System.Collections;
using MVVM.Extensions;
using MVVM.Factory;
using MVVM.Message;
using UnityEngine;

namespace MVVM
{
    public class CameraTest : MonoBehaviour
    {
        public SetupView SetupView;
        private TransientFactory transientFactory = new TransientFactory();

        private void Start()
        {
            SetupView.BindingContext = new SetupViewModel();
            SetupView.BindingContext.OnClick += () =>
            {
                Debug.Log("我点击了按钮");
            };
            //测试消息中心事件绑定
            MessageAggregator<object>.Instance. Sublisher("OnbtnClick", (sender, args) =>
            {
                Debug.Log(args._info);
            });
            //测试View生命周期
            SetupView.Show(false, () =>
            {
                Debug.Log("showed");
            });
            //测试ViewModel之间数据共享
            var enuma = SetupView.BindingContext.FindParent();
            
            //测试工厂模式
            var test1 = new Test();
            test1.TestFactory();
            var test2 = transientFactory.AcquireObject("Test") as Test;
            test2.TestFactory();
        }
    }
}