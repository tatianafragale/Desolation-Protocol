using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScProjectile : MonoBehaviour
{
    public ScEntity owner;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _rigidbody.velocity = transform.forward * 5;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<ScEntity>(out ScEntity otherEntity))
        {
            if (owner != null && owner.Team != otherEntity.Team)
            {
                otherEntity.TakeDamage(owner.Stats.damage);
            }
            else
            {
                otherEntity.TakeDamage(10f);
            }
        }
    }
}
