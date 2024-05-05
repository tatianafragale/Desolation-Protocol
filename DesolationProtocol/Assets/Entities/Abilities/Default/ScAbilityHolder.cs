using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScAbilityHolder : MonoBehaviour
{
    public ScEntity entity;
    public ScAbility[] ability;


    private void Awake()
    {
        entity = GetComponent<ScEntity>();
        foreach (ScAbility _ability in ability)
        {
            if (_ability)
            {
                _ability.cooldown.ResetCooldown();
            }
        }
    }

    public void TryAbility(int _selected)
    {
        if (_selected < ability.Length)
        {
            if (ability[_selected])
            {
                ability[_selected].Try(entity);
            }
            else
            {
                print("No Ability");
            }
        }
        else
        {
            print("No Slot");
        }
    }
}