using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScClawsHand : MonoBehaviour
{
    [SerializeField] private ScEntity Entity;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.TryGetComponent<ScEntity>(out ScEntity OtherEntity))
        {
            if (OtherEntity.Team != Entity.Team)
            {
                OtherEntity.TakeDamage(Entity.Stats.damage);
            }
        }
    }
}
