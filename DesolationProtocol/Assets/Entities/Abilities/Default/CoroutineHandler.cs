using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineHandler : MonoBehaviour
{
    private static CoroutineHandler _instance;

    public static CoroutineHandler Instance
    {
        get
        {
            if (_instance == null)
            {
                var obj = new GameObject("CoroutineHandler");
                _instance = obj.AddComponent<CoroutineHandler>();
                DontDestroyOnLoad(obj);
            }
            return _instance;
        }
    }

    public void StartRoutine(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }

    public void StopRoutine(IEnumerator coroutine)
    {
        StopCoroutine(coroutine);
    }

    public void StopAllRoutine(IEnumerator coroutine)
    {
        StopAllRoutine(coroutine);
    }
}
