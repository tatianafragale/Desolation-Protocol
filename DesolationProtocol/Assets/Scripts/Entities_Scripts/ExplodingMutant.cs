using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // Importante para el navmesh

public class ExplodingMutant : MonoBehaviour
{

    public NavMeshAgent Agent;
    public Transform Player;
    public LayerMask WhatIsGround, WhatIsPlayer;
    private bool _isExploding = false;
    [SerializeField] private float _attackRange;
    [SerializeField] private int _healthPoints;
    public bool PlayerInAttackRange;

    // VARIABLES PARA LA EXPLOSION
    public GameObject FatMutant, Explosion;

    

    private void Awake()
    {
        Player = GameObject.Find("PlayerAsset").transform;
        Agent = GetComponent<NavMeshAgent>();

        FatMutant.SetActive(true);
        Explosion.SetActive(false);
    }

    private void Update()
    {
        //Check if player is in attack range

        PlayerInAttackRange = Physics.CheckSphere(transform.position, _attackRange, WhatIsPlayer);

        if (!PlayerInAttackRange && !_isExploding) ChasePlayer();
        if (PlayerInAttackRange) ExplodingAttack();

    }

    private void ChasePlayer()
    {
        Agent.SetDestination(Player.position);
    }

    private void ExplodingAttack ()
    {
        Agent.SetDestination(transform.position);  // lo freno
        transform.LookAt(Player);

        _isExploding = true;  // uso este bool para que continue explotando aunque el jugador se escape

        // INSERTAR ANIM Y TIEMPO HASTA QUE EXPLOTE

        Explode();
    }

    private void Explode ()
    {
        Explosion.SetActive(true);
        FatMutant.SetActive(false);
        
    }

}
