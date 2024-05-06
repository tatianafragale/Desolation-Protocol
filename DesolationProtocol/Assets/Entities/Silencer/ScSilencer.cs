using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScSilencer : MonoBehaviour
{
    private ScEnemyChase chase;
    [SerializeField] private float walkDistance = 20f;
    [SerializeField] private float SilenceDistance = 40f;
    private Animator _anim;

    private void Awake()
    {
        chase = GetComponent<ScEnemyChase>();
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        DetectDistance();
    }

    private void DetectDistance()
    {
        if (Vector3.Distance(chase._target.position, transform.position) < SilenceDistance) 
        {
            chase._target.GetComponentInParent<ScEntity>().silenced = true;

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
        else
        {
            chase._target.GetComponentInParent<ScEntity>().silenced = false;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, walkDistance);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, SilenceDistance);
    }
}
