using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ScEntityClaws : ScEntityEnemy
{
    [SerializeField] private bool _playerInAttackRange = false;
    [SerializeField] private GameObject _claws;

    protected override void Awake()
    {
        base.Awake();
        KeepTracking();
        _claws.SetActive(false);
    }

    private void TryAttack()
    {
        if (health > 0)
        {
            if (_playerInAttackRange)
            {
                _anim.SetBool("Attacking", true);
                //_claws.SetActive(true);
            }
            else
            {
                KeepTracking();
                _claws.SetActive(false);
                _anim.SetBool("Attacking", false);
            }
            Invoke("TryAttack", Stats.attackSpeed);
        }
    }

    public void PlayerInRange(bool value)
    {
        if (health > 0)
        {
            _playerInAttackRange = value;
            if (_playerInAttackRange)
            {
                StopTracking();
                TryAttack();
            }
        }
    }

    protected override void Die()
    {
        base.Die();
        _claws.SetActive(false);
    }

    public void ActiveClaws()
    {
        _claws.SetActive(true);
    }

    public void DeactiveClaws()
    {
        _claws.SetActive(false);
    }
}
