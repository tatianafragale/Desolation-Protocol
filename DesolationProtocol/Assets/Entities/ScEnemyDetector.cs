using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScEnemyDetector : MonoBehaviour
{
    [SerializeField] private ScEntity Entity;
    [SerializeField] private UnityEvent EnterEvents;
    [SerializeField] private UnityEvent ExitEvents;

    private void OnTriggerEnter(Collider other)
    {
        ScEntity otherEntity = other.GetComponent<ScEntity>();
        if (otherEntity && Entity.Team != otherEntity.Team)
        {
            EnterEvents.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ScEntity otherEntity = other.GetComponent<ScEntity>();
        if (otherEntity && Entity.Team != otherEntity.Team)
        {
            ExitEvents.Invoke();
        }
    }
}
