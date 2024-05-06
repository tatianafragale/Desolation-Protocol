using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // Importante para el navmesh

public class ExplodingMutant : MonoBehaviour
{

    [SerializeField] private NavMeshAgent _agent;
    public Transform Player;
    public Transform Entity;
    public LayerMask WhatIsPlayer;
    private bool _isExploding = false;
    [SerializeField] private float _attackRange;
    [SerializeField] private float _healthPoints;
    [SerializeField] private bool _playerInAttackRange;

    private Animator _anim; // TENDRIA SENTIDO QUE ESTO SALGA DIRECTO DESDE ENTITY

    // VARIABLES PARA LA EXPLOSION
    public GameObject FatMutant, Explosion;

    private AudioSource _audioSource;

    [SerializeField] private float _explosionRange;
    [SerializeField] private int _maxHits = 30;
    [SerializeField] private int _maxDamage = 71;
    [SerializeField] private int _minDamage = 30;

    public LayerMask HitLayer;
    public LayerMask BlockExplosionsLayer;

    private Collider[] _entitiesHit;

    private void Awake()
    {
        Player = FindObjectOfType<ScPlayer>().transform;
        Entity = FindObjectOfType<ScEntity>().transform;
        _agent = GetComponent<NavMeshAgent>();

        _anim = GetComponentInChildren<Animator>();

        FatMutant.SetActive(true);
        Explosion.SetActive(false);

        _audioSource = GetComponent<AudioSource>();

        _entitiesHit = new Collider[_maxHits];
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


        // No Pega a traves de las paredes

        Ray _explosionRay = new Ray(transform.position, Entity.position);
        
        if (Physics.Raycast(_explosionRay, out RaycastHit hit))
        {
            int _totalHits = Physics.OverlapSphereNonAlloc(transform.position, _explosionRange, _entitiesHit, HitLayer);


            for (int i = 0; i < _totalHits; i++)
            {
                if (_entitiesHit[i].GetComponent<ScEntity>())
                {
                    float distance = Vector3.Distance(hit.point, _entitiesHit[i].transform.position);

                    if (!Physics.Raycast(hit.point, (_entitiesHit[i].transform.position - hit.point).normalized, distance, BlockExplosionsLayer.value))
                    {
                        _entitiesHit[i].GetComponent<ScEntity>().TakeDamage(Random.Range(_minDamage, _maxDamage));
                    }
                }
            }
        }

        Invoke("WaitForDestroy", 2);


        //Funciona pero tambien golpea a traves de las paredes - OBSOLETO
        /*
        Collider[] entitiesInRange = Physics.OverlapSphere(transform.position, _explosionRange);
        foreach (Collider entity in entitiesInRange)
        {
            if (entity.GetComponent<ScEntity>() != null)
            {
                entity.GetComponent<ScEntity>().TakeDamage(Random.Range(_minDamage, _maxDamage)); // Deberiamos agregar ragdoll al script de entity cuando se comen la explosion
            }

        }
        */
    }


    private void WaitForDestroy ()
    {
        Destroy(gameObject);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _explosionRange);
    }
}
