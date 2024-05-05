using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exploding_Enemy : EnemyMovement
{

    // VARIABLES PARA LA EXPLOSION
    private bool _isExploding = false;
    public GameObject FatMutant, Explosion;
    public Transform Entity;
    private AudioSource _audioSource;

    [SerializeField] private float _explosionRange;
    [SerializeField] private int _maxHits = 30;
    [SerializeField] private int _maxDamage = 71;
    [SerializeField] private int _minDamage = 30;

    public LayerMask HitLayer;
    public LayerMask BlockExplosionsLayer;

    private Collider[] _entitiesHit;

    void Update()
    {
        if (_playerInAttackRange) ExplodingAttack();
    }

    private void ExplodingAttack()
    {
        _agent.SetDestination(transform.position);  // lo freno

        _isExploding = true;  // uso este bool para que continue explotando aunque el jugador se escape

        _anim.SetTrigger("ExplodeAttack");

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


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _explosionRange);
    }
}
