using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

[CreateAssetMenu(fileName = "Healing", menuName = "Abilities/Sustain/Healing")]
public class ScAbilityHealing : ScAbility
{
    [SerializeField] private float percent;
    [SerializeField] private float duration;

    public override void Try(ScEntity entity)
    {
        if (entity.health < entity.Stats.maxHealth)
        {
            base.Try(entity);
        }
    }
    protected override void Activate(ScEntity entity)
    {
        base.Activate(entity);
        StartCoroutine(HealingPerTime(entity));
    }

    private IEnumerator HealingPerTime(ScEntity entity)
    {
        float timer = 0f;

        while (timer < duration)
        {
            entity.Heal(percent * entity.Stats.maxHealth * Time.fixedDeltaTime);
            timer += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
    }
}