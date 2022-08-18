using System;
using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using UnityEngine;

public class CameraTest : MonoBehaviour
{
    
    /// <summary>
    /// 控制主摄像机
    /// </summary>
    public Camera mainCamera;

    /// <summary>
    /// 正交摄像机最大缩放参数
    /// </summary>
    [Header("最大缩放")]
    public float CameraZoomMax;
    /// <summary>
    /// 正交摄像机最小缩放参数
    /// </summary>
    [Header("最小缩放")]
    public float CameraZoomMin;
    /// <summary>
    /// 摄像机正交状态远近
    /// </summary>
    [Header("控制摄像机正交默认尺寸")] 
    public float conrtrol_camera_size;
    /// <summary>
    /// 摄像机默认位置
    /// </summary>
    [Header("控制摄像机正交默认尺寸")] 
    public Vector3 control_camera_pos;                     
    
    
    
    private PinchGesture pinch_gesture;
    private SwipeGesture swipe_gesture;
    private void Awake()
    {
        InitCameraSize();
        
        pinch_gesture = new PinchGesture(GRoot.inst);
        swipe_gesture = new SwipeGesture(GRoot.inst);
        swipe_gesture.onMove.AddCapture(Capture);
        pinch_gesture.onAction.AddCapture(Gesture);
        Stage.inst.onMouseWheel.AddCapture(MouseWheelZoom);

    }
    /// <summary>
    /// 滑动摄像机
    /// </summary>
    /// <param name="context"></param>
    private void Capture(EventContext context)
    {
        //if(Stage.isTouchOnUI) return;
        var tmp = swipe_gesture.delta;
        var factor = mainCamera.orthographicSize / Screen.height * 2;
        mainCamera.transform.position += new Vector3(-tmp.x, tmp.y, 0) * factor;
    }

    /// <summary>
    /// 两指缩放
    /// </summary>
    /// <param name="context"></param>
    private void Gesture(EventContext context)
    {
        //if(Stage.isTouchOnUI) return;
        var size = Mathf.Clamp(mainCamera.orthographicSize - pinch_gesture.delta * 10, CameraZoomMin, CameraZoomMax);
        mainCamera.orthographicSize = size;
    }

    private void MouseWheelZoom(EventContext context)
    {
        //if(Stage.isTouchOnUI) return;
        mainCamera.orthographicSize = Miscs.wrap<float>(mainCamera.orthographicSize + 0.05f * context.inputEvent.mouseWheelDelta, CameraZoomMin, CameraZoomMax);   
    }
    
    /// <summary>
    /// 初始化摄像机的状态，位置，缩放
    /// </summary>
    public void InitCameraSize()
    {
        mainCamera.orthographicSize = conrtrol_camera_size;
        mainCamera.transform.localPosition = control_camera_pos;
    }
    /// <summary>
    /// 限制摄像机移动范围
    /// </summary>
    public void LimitPosition()
    {
        
    }
    /// <summary>
    /// 限制摄像机的缩放范围
    /// </summary>
    /// <param name="v2">v2.x = min; v2.y = max</param>
    public void LimitZoom(Vector2 v2)
    {
        
    }
    private void Update()
    {
        if (FairyGUI.Stage.isTouchOnUI) 
            return;
        ClickLive2d();
    }
        
    private void ClickLive2d()
    {
        if (Input.GetMouseButtonUp(0))
        {
            MouseRaycast();
        }
    }
    // 检测点击的是否是可点击的部件
    public void MouseRaycast()
    {
        //通知UI 返回上一级菜单
        Canle();
    }
    /// <summary>
    /// 返回上一级菜单键
    /// </summary>
    public void Canle()
    {
        
    }

    private void OnDestroy()
    {
        swipe_gesture.onMove.RemoveCapture(Capture);
        pinch_gesture.onAction.RemoveCapture(Gesture);
        Stage.inst.onMouseWheel.RemoveCapture(MouseWheelZoom);
    }
}
public class Miscs
{
    public static T wrap<T>(T v, T min, T max) where T : System.IComparable<T> {
        if (v.CompareTo(min) < 0) return min;
        if (v.CompareTo(max) > 0) return max;
        return v;
    }
}