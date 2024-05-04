using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // Importante para el navmesh

public class ExplodingMutant : MonoBehaviour
{

    public NavMeshAgent Agent;
    public Transform Player;
    public LayerMask WhatIsPlayer;
    private bool _isExploding = false;
    [SerializeField] private float _attackRange;
    [SerializeField] private int _healthPoints;
    [SerializeField] private bool _playerInAttackRange;

    [SerializeField] private int _explosionFrame = 111;

    private Animator _anim; // TENDRIA SENTIDO QUE ESTO SALGA DIRECTO DESDE ENTITY

    // VARIABLES PARA LA EXPLOSION
    public GameObject FatMutant, Explosion;

    private AudioSource _audioSource;

    

    private void Awake()
    {
        Player = GameObject.Find("PlayerAsset").transform;
        Agent = GetComponent<NavMeshAgent>();
        
        _anim = GetComponentInChildren<Animator>();

        FatMutant.SetActive(true);
        Explosion.SetActive(false);

        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        //Check if player is in attack range

        _playerInAttackRange = Physics.CheckSphere(transform.position, _attackRange, WhatIsPlayer);

        if (!_playerInAttackRange && !_isExploding) ChasePlayer();
        if (_playerInAttackRange) ExplodingAttack();

    }

    private void ChasePlayer()
    {
        Agent.SetDestination(Player.position);
    }

    private void ExplodingAttack ()
    {
        Agent.SetDestination(transform.position);  // lo freno
        //transform.LookAt(Player);

        _isExploding = true;  // uso este bool para que continue explotando aunque el jugador se escape

        // INSERTAR ANIM Y TIEMPO HASTA QUE EXPLOTE

        _anim.SetTrigger("ExplodeAttack");

    }

    public void Explode ()
    {
        Explosion.SetActive(true);
        FatMutant.SetActive(false);

        _audioSource.Play();
    }

}
