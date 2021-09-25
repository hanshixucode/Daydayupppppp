using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Single<T> where T: class,new()
{
    protected static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new T();
            }

            return _instance;
        }
    }
}

public class SingleMono<T> : MonoBehaviour where T : MonoBehaviour
{
    //Monobehavior singleon
}
