using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.TextCore.Text;

[CreateAssetMenu]
public class ScAbilityDash : ScAbility
{
    [SerializeField] private float dashSpeed;
    public override void Activate(ScEntity entity)
    {
        base.Activate(entity);
        Rigidbody _rigidbody = entity.GetComponent<Rigidbody>();
        _rigidbody.velocity = new Vector3(0, 0, 0);
        _rigidbody.AddForce(_rigidbody.transform.forward * dashSpeed * 1000);
    }
}