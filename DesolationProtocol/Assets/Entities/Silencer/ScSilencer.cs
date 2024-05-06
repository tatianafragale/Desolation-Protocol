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

    // Start is called before the first frame update
    void Start()
    {
        DetectDistance();
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
    }

    
}
