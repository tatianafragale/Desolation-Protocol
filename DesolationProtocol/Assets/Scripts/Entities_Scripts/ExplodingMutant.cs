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
    public bool PlayerInAttackRange;

    

    private void Awake()
    {
        Player = GameObject.Find("PlayerAsset").transform;
        Agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //Check if player is in attack range

        PlayerInAttackRange = Physics.CheckSphere(transform.position, _attackRange, WhatIsPlayer);

        if (!PlayerInAttackRange && !_isExploding) ChasePlayer();
        else Explode();
    }

    private void ChasePlayer()
    {

    }

    private void Explode ()
    {

    }

}
