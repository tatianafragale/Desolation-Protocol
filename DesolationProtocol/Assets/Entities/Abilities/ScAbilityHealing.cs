using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ScAbilityHealing : ScAbility
{
    [SerializeField] private float percent;

    public override void Try(ScEntity entity)
    {
        if (entity.health <= entity.Stats.maxHealth * 0.95)
        {
            base.Try(entity);
        }
    }
    public override void Activate(ScEntity entity)
    {
        base.Activate(entity);
        entity.Heal(percent * entity.Stats.maxHealth);
    }
}


// Hago cambio prueba