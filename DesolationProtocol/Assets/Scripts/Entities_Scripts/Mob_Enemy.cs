using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_Enemy : EnemyMovement
{
    private bool _alreadyAttacked = false;
    [SerializeField] private float _timeBetweenAttacks;
    [SerializeField] private int _maxAttackDMG;
    [SerializeField] private int _minAttackDMG;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerInAttackRange) Attack();
    }

    private void Attack ()
    {
        _agent.SetDestination(transform.position);
        transform.LookAt(Player);

        if (!_alreadyAttacked)
        {
            _anim.SetTrigger("Attack");
            _alreadyAttacked = true;
            Invoke(nameof(ResetAttack), _timeBetweenAttacks);
        }
    }

    private void DamagePoint()
    {
        GetComponent<ScEntity>().TakeDamage(Random.Range(_minAttackDMG, _maxAttackDMG));
    }
    
    private void ResetAttack()
    {
        _alreadyAttacked = false;
    }
}
