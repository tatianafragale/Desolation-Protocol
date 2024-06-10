using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static UnityEngine.EventSystems.EventTrigger;

[CreateAssetMenu(fileName = "Dash", menuName = "Abilities/Movement/Dash")]
public class ScAbilityDash : ScAbility
{
    [SerializeField] private float strength;
    [SerializeField] private float duration;
    protected override void Activate(ScEntity entity)
    {
        base.Activate(entity);
        StartCoroutine(Dashing(entity));
    }

    private IEnumerator Dashing(ScEntity entity)
    {
        Rigidbody _rigidbody = entity.GetComponent<Rigidbody>();
        _rigidbody.velocity = new Vector3(0, _rigidbody.velocity.y, 0);
        Vector3 direction = _rigidbody.transform.forward;

        float timer = 0f;
        while (timer < duration)
        {
            _rigidbody.AddForce(direction * strength, ForceMode.Impulse);
            timer += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
    }
}