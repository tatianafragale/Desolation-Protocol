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
        _rigidbody.velocity = transform.forward * 100;
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<ScEntity>(out ScEntity otherEntity))
        {
            if (owner != null)
            {
                if (owner.Team != otherEntity.Team)
                {
                    otherEntity.TakeDamage(owner.Stats.damage);
                }
            }
            else
            {
                otherEntity.TakeDamage(10f);
            }
        }
        Destroy(gameObject);
    }
}
