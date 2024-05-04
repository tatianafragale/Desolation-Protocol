using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // Importante para el navmesh

public class ExplodingMutant : MonoBehaviour
{

    [SerializeField] private NavMeshAgent _agent;
    public Transform Player;
    public LayerMask WhatIsPlayer;
    private bool _isExploding = false;
    [SerializeField] private float _attackRange;
    [SerializeField] private float _healthPoints;
    [SerializeField] private bool _playerInAttackRange;

    [SerializeField] private int _explosionFrame = 111;

    private Animator _anim; // TENDRIA SENTIDO QUE ESTO SALGA DIRECTO DESDE ENTITY

    // VARIABLES PARA LA EXPLOSION
    public GameObject FatMutant, Explosion;

    private AudioSource _audioSource;

    [SerializeField] private float _explosionRange;



    private void Awake()
    {
        Player = FindObjectOfType<ScPlayer>().transform;
        _agent = GetComponent<NavMeshAgent>();

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
        _agent.SetDestination(Player.position);
    }

    private void ExplodingAttack()
    {
        _agent.SetDestination(transform.position);  // lo freno

        _isExploding = true;  // uso este bool para que continue explotando aunque el jugador se escape

        _anim.SetTrigger("ExplodeAttack");

    }

    public void TakeDamage(float incomingDamage) // ESTO EN REALIDAD HAY QUE HACERLO CON EL SCRIPT DE ENTITY
    {

        _healthPoints -= incomingDamage;
        
        //Validate Death
        if (_healthPoints <= 0)
        {
            Explode();
        }
    }

    public void Explode()
    {
        Explosion.SetActive(true);
        FatMutant.SetActive(false);

        _audioSource.Play();

        Collider[] entitiesInRange = Physics.OverlapSphere(transform.position, _explosionRange);

        foreach (Collider entity in entitiesInRange)
        {
            if (entity.GetComponent<ScEntity>() != null)
            {
                entity.GetComponent<ScEntity>().TakeDamage(70); // Deberiamos agregar ragdoll al script de entity cuando se comen la explosion
            }

        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _explosionRange);
    }
}
