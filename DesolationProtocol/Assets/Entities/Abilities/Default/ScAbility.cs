using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScAbility : ScriptableObject
{
    public string title;
    public Sprite icon;
    [SerializeField] private float cooldownTime = 10f;
    public ScCooldown cooldown;

    public virtual void Try(ScEntity entity)
    {
        if (cooldown.IsReady)
        {
            Activate(entity);
        }
        else
        {
            Debug.Log("On CD Sound");
        }
    }

    protected virtual void Activate(ScEntity entity)
    {
        cooldown.StartCooldown(cooldownTime/ entity.Stats.cooldowns);
    }

    public virtual void Cancel()
    {
        
    }

    public void StartCoroutine(IEnumerator coroutine)
    {
        CoroutineHandler.Instance.StartRoutine(coroutine);
    }

    public void StopRoutine(IEnumerator coroutine)
    {
        CoroutineHandler.Instance.StopRoutine(coroutine);
    }
}