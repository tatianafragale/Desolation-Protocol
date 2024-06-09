using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScSilencerCollider : MonoBehaviour
{
    private ScEntitySilencer Silencer;
    public SphereCollider SphereCollider;

    private void Awake()
    {
        Silencer = GetComponentInParent<ScEntitySilencer>();
        SphereCollider = GetComponent<SphereCollider>();
    }

    private void Start()
    {
        SphereCollider.radius = Silencer.silenceDistance;
    }

    private void OnTriggerEnter(Collider other)
    {
        Silencer.Silence(other, true);
    }

    private void OnTriggerExit(Collider other)
    {
        Silencer.Silence(other, false);
    }
}
