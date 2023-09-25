using System.Collections;
using System.Collections.Generic;
using System.IO;
using IFix;
using IFix.Core;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    void Start()
    {
        string text = Path.Combine(Application.streamingAssetsPath, "test.patch.bytes");
        bool flag = File.Exists(text);
        if (flag)
        {
            Debug.Log("Load HotFix, patchPath=" + text);
            PatchManager.Load(new FileStream(text, FileMode.Open), true);
        }
    }

    void Update()
    {
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect((Screen.width - 200) / 2, 20, 200, 100), "Call  FuncA"))
        {
            Debug.Log("Button, Call FuncA, result=" + FuncA());
        }
    }
    public string FuncA()
    {
        return "Old";
    }
}