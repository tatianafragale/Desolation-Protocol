using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // Importante para el navmesh

public class ScEntityExplosive : ScEntityEnemy
{
    private bool _isExploding = false;

    //variables
    [SerializeField] private float _explosionRange;
    [SerializeField] private bool doRaycast = true;
    [SerializeField] private GameObject FatMutant, Explosion;
    [SerializeField] private LayerMask PlayerLayer, DmgLayer, BlockLayer;

    protected override void Awake()
    {
        base.Awake();
        _anim = GetComponentInChildren<Animator>();

        FatMutant.SetActive(true);
        Explosion.SetActive(false);

        _audioSource = GetComponent<AudioSource>();

    }

    protected override void Update()
    {
        //Check if player is in attack range
        base.Update();
        bool _playerInAttackRange = Physics.CheckSphere(transform.position, _explosionRange, PlayerLayer);
        if (!_isExploding)
        {
            if (!_playerInAttackRange)
            {
                KeepTracking();
            }
            else
            {
                StopTracking();
                _isExploding = true;
                _anim.SetTrigger("ExplodeAttack");
            }
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
            if (entity.GetComponent<ScEntity>() != null && entity.GetComponent<ScEntity>() != this)
            {
                ScEntity LocalEntity = entity.GetComponent<ScEntity>();
                if (doRaycast)
                {
                    Debug.DrawRay(transform.position, (LocalEntity.transform.position - transform.position).normalized * _explosionRange, Color.green);

                    Ray ray = new Ray(transform.position, (LocalEntity.transform.position - transform.position).normalized * _explosionRange);
                    RaycastHit DmgHit;
                    Physics.Raycast(ray, out DmgHit, _explosionRange, DmgLayer);
                    RaycastHit BlockHit;
                    Physics.Raycast(ray, out BlockHit, _explosionRange, BlockLayer);

                    if (DmgHit.distance < BlockHit.distance || !BlockHit.collider)
                    {
                        LocalEntity.TakeDamage(Random.Range(Stats.damage, Stats.damage * Stats.critMultiplier));
                    }
                }
                else
                {
                    LocalEntity.TakeDamage(Random.Range(Stats.damage, Stats.damage * Stats.critMultiplier));
                }
            }
        }
        Destroy(gameObject, 2);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _explosionRange);
    }
}
