using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ScEnemyChase : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    public Transform _target;
    private ScEntity _entity;
    public bool _active = true;

    private void Awake()
    {
        _target = FindObjectOfType<ScPlayer>().transform;
        _agent = GetComponent<NavMeshAgent>();
        _entity = GetComponent<ScEntity>();
        _agent.speed = _entity.Stats.movementSpeed;
    }

    private void Start()
    {
        //_agent.speed = _entity.Stats.movementSpeed; EN CASO DE Q NO ANDE CON AWAKE
    }

    private void Update()
    {
        if (_active)
        {
            if (_target != null)
            {
                _target = FindObjectOfType<ScPlayer>().transform;
            }
            if (_target)
            {
                _agent.SetDestination(_target.position);
            }
        }
    }

    public void KeepMoving()
    {
        _active = true;
        _agent.SetDestination(_target.position);
    }

    public void Stop()
    {
        _active = false;
        _agent.SetDestination(transform.position);
    }
}
