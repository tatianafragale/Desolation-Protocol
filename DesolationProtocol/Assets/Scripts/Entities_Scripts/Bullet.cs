using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed;
    [SerializeField] private float _damage = 5;


    void Awake()
    {

    }

    void Update()
    {
        transform.position += transform.forward * Speed * Time.deltaTime;


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<ScEntity>(out ScEntity Entity))
        {
            Entity.TakeDamage(_damage);
        }

        Destroy(gameObject);
    }

}
