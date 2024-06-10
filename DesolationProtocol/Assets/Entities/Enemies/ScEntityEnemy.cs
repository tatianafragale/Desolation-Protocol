using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ScEntityEnemy : ScEntity
{
    private NavMeshAgent _agent;
    protected Transform _target;
    protected bool _active = true;
    protected bool rotateidle = false;

    protected override void Awake()
    {
        base.Awake();
        _agent = GetComponent<NavMeshAgent>();
        _target = FindObjectOfType<ScEntityPlayer>().transform;
        _agent.speed = Stats.movementSpeed;
    }

    protected override void Update()
    {
        if (_active)
        {
            if (_target)
            {
                _agent.SetDestination(_target.position);
            }
            else
            {
                _target = FindObjectOfType<ScEntityPlayer>().transform;
            }
        }
    }

    protected override void Die()
    {
        base.Die();
        StopTracking();
        Destroy(gameObject, 5);
    }

    protected void KeepTracking()
    {
        if (health > 0)
        {
            _agent.speed = Stats.movementSpeed;
            _active = true;
            _agent.SetDestination(_target.position);
        }
    }

    protected void StopTracking()
    {
        _active = false;
        _agent.SetDestination(transform.position);
    }

    protected void RotateTracking()
    {
        if (health > 0)
        {
            _agent.speed = 0;
            _active = false;
            _agent.SetDestination(transform.position);
        }
    }
}
