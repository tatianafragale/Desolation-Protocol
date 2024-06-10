using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Teleport", menuName = "Abilities/Movement/Teleport")]
public class ScAbilityTeleport : ScAbility
{
    [SerializeField] private float distance;
    protected override void Activate(ScEntity entity)
    {
        base.Activate(entity);
        Rigidbody _rigidbody = entity.GetComponent<Rigidbody>();
        _rigidbody.velocity = new Vector3(0, 0, 0);
        _rigidbody.transform.position = _rigidbody.transform.position + _rigidbody.transform.forward * distance;
    }
}