using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] protected NavMeshAgent _agent;
    public Transform Player;
    [SerializeField] protected LayerMask WhatIsPlayer;
    [SerializeField] private float _attackRange;
    [SerializeField] private float _healthPoints;
    protected bool _playerInAttackRange;

    protected Animator _anim;

    private AudioSource _audioSource;

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

    }

    private void ChasePlayer()
    {
        _agent.SetDestination(Player.position);
    }

}
