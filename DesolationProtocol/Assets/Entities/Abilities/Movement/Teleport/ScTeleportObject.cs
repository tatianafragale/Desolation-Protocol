using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScTeleportObject : MonoBehaviour
{
    private Rigidbody _rigidbody;
    [SerializeField] public bool _moving = true;
    [SerializeField] public float strength;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (_moving && _rigidbody)
        {
            _rigidbody.AddForce(_rigidbody.transform.forward * strength);
        }
    }
}
