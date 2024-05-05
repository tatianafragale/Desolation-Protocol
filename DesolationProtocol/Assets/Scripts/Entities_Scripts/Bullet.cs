using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed;

    void Start()
    {
        
    }

    void Update()
    {
        transform.position += transform.forward * Speed * Time.deltaTime;


    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }

}
