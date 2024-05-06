using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // Importante para el navmesh

public class ExplodingMutant : MonoBehaviour
{
    private ScEntity _entity;
    private ScEnemyChase _enemyChase;
    private bool _isExploding = false;

    //variables
    [SerializeField] private float _explosionRange;
    [SerializeField] private bool doRaycast = true;
    [SerializeField] private GameObject FatMutant, Explosion;
    [SerializeField] private LayerMask PlayerLayer, DmgLayer, BlockLayer;

    private Animator _anim; // TENDRIA SENTIDO QUE ESTO SALGA DIRECTO DESDE ENTITY

    private AudioSource _audioSource;

    private void Awake()
    {
        _entity = GetComponent<ScEntity>();
        _enemyChase = GetComponent<ScEnemyChase>();

        _anim = GetComponentInChildren<Animator>();

        FatMutant.SetActive(true);
        Explosion.SetActive(false);

        _audioSource = GetComponent<AudioSource>();

    }

    private void Update()
    {
        //Check if player is in attack range

        bool _playerInAttackRange = Physics.CheckSphere(transform.position, _explosionRange, PlayerLayer);
        if (!_isExploding)
        {
            if (!_playerInAttackRange)
            {
                _enemyChase.KeepMoving();
            }
            else
            {
                _enemyChase.Stop();
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
            if (entity.GetComponent<ScEntity>() != null && entity.GetComponent<ScEntity>() != _entity)
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
                        LocalEntity.TakeDamage(Random.Range(_entity.Stats.damage, _entity.Stats.damage * _entity.Stats.critMultiplier));
                    }
                }
                else
                {
                    LocalEntity.TakeDamage(Random.Range(_entity.Stats.damage, _entity.Stats.damage * _entity.Stats.critMultiplier));
                }
            }
        }
        Invoke("WaitForDestroy", 2);
    }

    private void WaitForDestroy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _explosionRange);
    }
}
