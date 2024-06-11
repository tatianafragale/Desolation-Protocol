using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScDetectorCollider : MonoBehaviour
{
    [SerializeField] private UnityEvent EnterEvents;
    [SerializeField] private UnityEvent ExitEvents;

    private void OnTriggerEnter(Collider other)
    {
        EnterEvents.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        ExitEvents.Invoke();
    }
}
