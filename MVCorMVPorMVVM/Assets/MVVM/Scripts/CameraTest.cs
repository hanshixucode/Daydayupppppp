using System;
using System.Collections;
using MVVM.Extensions;
using MVVM.Factory;
using MVVM.Inject;
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
            // var test1 = new Test();
            // test1.TestFactory();
            // var test2 = transientFactory.AcquireObject("Test") as Test;
            // test2.TestFactory();
            //
            // var pool = new PoolFactory(3, true);
            // var obj1 =  pool.AcquireObject(typeof(Test)) as Test;
            // obj1.index = 1;
            // var obj2 =  pool.AcquireObject(typeof(Test));
            // var obj3 =  pool.AcquireObject(typeof(Test));
            // pool.ReleaseObject(obj1);
            // var obj4 =  pool.AcquireObject(typeof(Test))as Test;
            // obj4.index = 4;
            // Debug.Log(obj4.index);
            
            //测试注入
            ServiceLocator.RegisterTransient<Test>();
            var obj5 = ServiceLocator.ResolveInstance<Test>();
            obj5.index = 123;
            obj5.TestFactory();
            var obj6 = ServiceLocator.ResolveInstance<Test>();
            Debug.Log($" obj5 is {obj5.index}, obj6 is {obj6.index}");
        }
    }
}