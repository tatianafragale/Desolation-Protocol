using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScSilencerCollider : MonoBehaviour
{
    private ScSilencer Silencer;
    public SphereCollider SphereCollider;

    private void Awake()
    {
        Silencer = GetComponentInParent<ScSilencer>();
        SphereCollider = GetComponent<SphereCollider>();
    }

    private void Start()
    {
        SphereCollider.radius = Silencer.SilenceDistance;
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
