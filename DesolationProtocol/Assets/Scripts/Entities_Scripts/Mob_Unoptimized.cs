using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mob_Unoptimized : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Transform Player;
    [SerializeField] private LayerMask WhatIsPlayer;
    [SerializeField] private float _attackRange;
    [SerializeField] private float _healthPoints;


    private bool _playerInAttackRange;

    private Animator _anim;

    private AudioSource _audioSource;

    //Attack Variables
    private bool _alreadyAttacked = false;
    [SerializeField] private float _timeBetweenAttacks;
    [SerializeField] private Collider _claws;
    //[SerializeField] private int _maxAttackDMG;
    //[SerializeField] private int _minAttackDMG;

    private bool _enemyInHitDistance;

    private void Awake()
    {
        Player = FindObjectOfType<ScPlayer>().transform;
        _agent = GetComponent<NavMeshAgent>();

        _anim = GetComponentInChildren<Animator>();

        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        //Check if player is in attack range

        _playerInAttackRange = Physics.CheckSphere(transform.position, _attackRange, WhatIsPlayer);

        if (!_playerInAttackRange) ChasePlayer();
        if (_playerInAttackRange) Attack();
    }

    private void ChasePlayer()
    {
        _agent.SetDestination(Player.position);
    }

    private void Attack()
    {
        _agent.SetDestination(transform.position); // No se si en este caso queremos frenarlo. Tal vez haga que nunca le pegue
        //transform.LookAt(Player);

        if (!_alreadyAttacked)
        {
            _anim.SetTrigger("Attack");
            _alreadyAttacked = true;
            _claws.enabled = true;
            Invoke(nameof(ResetAttack), _timeBetweenAttacks);
        }
    }
    /*private void DamagePoint()
    {
        _enemyInHitDistance = Physics.CheckSphere(transform.position, _dmgRange, WhatIsPlayer);

        if (_enemyInHitDistance)
        {
            Player.GetComponent<ScEntity>().TakeDamage(Random.Range(_minAttackDMG, _maxAttackDMG));
        }
    }*/

    private void DisableClaws()
    {
        _claws.enabled = false;
    }

    private void ResetAttack()
    {
        _alreadyAttacked = false;
    }
}
