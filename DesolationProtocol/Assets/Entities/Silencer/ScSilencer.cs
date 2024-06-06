using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class ScSilencer : MonoBehaviour
{
    private ScEnemyChase chase;
    private ScEntity Entity;
    [SerializeField] private float walkDistance = 20f;
    [SerializeField] public float SilenceDistance = 40f;
    private Animator _anim;
    [SerializeField] private LayerMask layerMask;

    private void Awake()
    {
        Entity = GetComponent<ScEntity>();
        chase = GetComponent<ScEnemyChase>();
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(chase._target.position, transform.position) < walkDistance)
        {
            chase.Stop();
            _anim.SetBool("InRange", true);
        }
        else
        {
            chase.KeepMoving();
            _anim.SetBool("InRange", false);
        }
    }

    public void Silence(Collider other, bool action)
    {
        ScEntity otherEntity = other.GetComponent<ScEntity>();
        if (otherEntity && Entity.Team != otherEntity.Team)
        {
            otherEntity.silenced = action;
            print(action);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, walkDistance);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, SilenceDistance);
    }

    private void OnDestroy()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, SilenceDistance, layerMask);

        foreach (Collider collider in hitColliders)
        {
            Silence(collider, false);
        }
    }
}
