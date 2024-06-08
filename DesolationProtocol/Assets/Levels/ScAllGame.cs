using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScAllGame : MonoBehaviour
{
    public static ScAllGame Instance;

    private void Awake()
    {
        if (ScAllGame.Instance == null)
        {
            ScAllGame.Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
