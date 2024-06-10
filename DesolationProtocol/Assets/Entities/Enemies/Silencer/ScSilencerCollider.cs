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
        ScEntity otherEntity = other.GetComponent<ScEntity>();
        if (otherEntity && Silencer.Team != otherEntity.Team)
        {
            Silencer.Silence(other, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ScEntity otherEntity = other.GetComponent<ScEntity>();
        if (otherEntity && Silencer.Team != otherEntity.Team)
        {
            Silencer.Silence(other, false);
        }
    }
}
