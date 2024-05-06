using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScEntityJump : MonoBehaviour
{
    private ScEntity _entity;
    private int i;

    private void Awake()
    {
        _entity = GetComponentInParent<ScEntity>();
    }

    private void OnTriggerEnter(Collider other)
    {
        i++;
        _entity.OnLand();
    }

    private void OnTriggerExit(Collider collision)
    {
        i--;
        if (i == 0)
        {
            _entity.OnAir();
        }
    }
}
