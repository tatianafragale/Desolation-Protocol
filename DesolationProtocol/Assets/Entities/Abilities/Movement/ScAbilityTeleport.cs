using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Teleport", menuName = "Abilities/Movement/Teleport")]
public class ScAbilityTeleport : ScAbility
{
    [SerializeField] private float strength = 5;
    [SerializeField] private float timeActive = 30;
    [SerializeField] private GameObject teleport;
    [SerializeField] private GameObject target;

    public override void Try(ScEntity entity)
    {
        if (!target)
        {
            base.Try(entity);
        }
        else
        {
            Rigidbody _rigidbody = entity.GetComponent<Rigidbody>();
            _rigidbody.velocity = new Vector3(0, 0, 0);
            _rigidbody.transform.position = target.transform.position + Vector3.up;
            Destroy(target);
        }
    }

    protected override void Activate(ScEntity entity)
    {
        base.Activate(entity);
        target = Instantiate(teleport, entity.transform.position, entity.transform.rotation);
        target.GetComponent<ScTeleportObject>().strength = strength;
        Destroy(target, timeActive);
    }
}