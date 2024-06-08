using UnityEngine;
using static UnityEngine.LightAnchor;



public class ScTank : MonoBehaviour
{
    private ScEnemyChase chase;
    private Rigidbody rb;

    public Transform Player;

    private Animator _anim;

    private void Awake()
    {
        Player = FindObjectOfType<ScWeapon>().transform;
        _anim = GetComponent<Animator>();
        chase = GetComponent<ScEnemyChase>();
    }


    /*private void Start()
    {
        Invoke("die", 5f);
    }
    */

    private void Update()
    {
        if (Vector3.Distance(chase._target.position, transform.position) < 3)
        {
            if (chase._active)
            {
                chase.Stop();
                _anim.SetBool("InRange", true);
            }
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Player.position - transform.position), Time.deltaTime * 0.25f);
        }
        else
        {
            if (!chase._active)
            {
                chase.KeepMoving();
                _anim.SetBool("InRange", false);
            }
        }
    }
    
    public void die()
    {
        _anim.SetTrigger("dead");
    }


}
