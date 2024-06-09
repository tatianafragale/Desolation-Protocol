using UnityEngine;

public class ScEntityTank : ScEntityEnemy
{
    protected override void Update()
    {
        base.Update();
        if (Vector3.Distance(_target.position, transform.position) < 3)
        {
            if (_active)
            {
                RotateTracking();
                _anim.SetBool("InRange", true);
            }
        }
        else
        {
            if (!_active)
            {
                KeepTracking();
                _anim.SetBool("InRange", false);
            }
        }
    }
}
