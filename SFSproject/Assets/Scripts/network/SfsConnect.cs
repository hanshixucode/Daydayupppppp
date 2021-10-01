using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sfs2X;
using Sfs2X.Core;
using UnityEditor;

public class SfsConnect : MonoBehaviour
{
    // Start is called before the first frame update
    public string ServerIP = "127.0.0.1";
    public int ServerPort = 9933;

    private SmartFox sfs;
    void Start()
    {
        sfs = new SmartFox();
        sfs.ThreadSafeMode = true;
        
        sfs.AddEventListener(SFSEvent.CONNECTION,OnConnection);
        
        sfs.Connect(ServerIP,ServerPort);
        
        
    }

    void OnConnection(BaseEvent e)
    {
        if ((bool) e.Params["success"])
        {
            Debug.Log("successfully connected");
        }
        else
        {
            Debug.Log("failed");
        }
    }

    // Update is called once per frame
    void Update()
    {
        sfs.ProcessEvents();
        
    }

    private void OnApplicationQuit()
    {
        if(sfs.IsConnected)
            sfs.Disconnect();
    }
}
