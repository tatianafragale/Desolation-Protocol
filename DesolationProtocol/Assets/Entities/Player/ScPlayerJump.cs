using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScPlayerJump : MonoBehaviour
{
    private ScPlayer _player;
    private int i;

    private void Awake()
    {
        _player = GetComponentInParent<ScPlayer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        i++;
        _player.OnLand();
    }

    private void OnTriggerExit(Collider collision)
    {
        i--;
        if (i == 0)
        {
            _player.OnAir();
        }
    }
}
