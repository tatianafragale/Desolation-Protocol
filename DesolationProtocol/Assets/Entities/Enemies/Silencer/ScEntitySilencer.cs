using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class ScEntitySilencer : ScEntityEnemy
{
    [SerializeField] private float walkDistance = 20f;
    [SerializeField] public float silenceDistance = 40f;
    [SerializeField] private LayerMask layerMask;

    protected override void Update()
    {
        base.Update();
        if (Vector3.Distance(_target.position, transform.position) < walkDistance)
        {
            StopTracking();
            _anim.SetBool("InRange", true);
        }
        else
        {
            KeepTracking();
            _anim.SetBool("InRange", false);
        }
    }

    public void Silence(Collider other, bool action)
    {
        if (health > 0)
        {
            ScEntity otherEntity = other.GetComponent<ScEntity>();
            if (otherEntity && Team != otherEntity.Team)
            {
                if (action)
                {
                    otherEntity.silencers++;
                }
                else
                {
                    otherEntity.silencers--;
                }
            }
        }
    }

    protected override void Die()
    {
        base.Die();
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, silenceDistance, layerMask);

        foreach (Collider collider in hitColliders)
        {
            ScEntity otherEntity = collider.GetComponent<ScEntity>();
            if (otherEntity && Team != otherEntity.Team)
            {
                otherEntity.silencers--;
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, walkDistance);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, silenceDistance);
    }
}
