using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Teleport", menuName = "Abilities/Movement/Teleport")]
public class ScAbilityTeleport : ScAbility
{
    [SerializeField] private float strength = 5;
    [SerializeField] private float timeActive = 30;
    [SerializeField] private GameObject teleport;
    private GameObject target;

    public override void Try(ScEntity entity)
    {
        if (!target)
        {
            base.Try(entity);
        }
        else
        {
            Rigidbody rigidbody = entity.GetComponent<Rigidbody>();
            rigidbody.velocity = new Vector3(0, 0, 0);
            rigidbody.transform.position = target.transform.position;
            Destroy(target);
            target = null;
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