using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scpanel : MonoBehaviour
{
    [SerializeField] private ScTurret Turret;

    private void OnTriggerEnter(Collider other)
    {
        Turret.TurnOff();
    }
}
