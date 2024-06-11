using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class ScAbility : ScriptableObject
{
    [SerializeField] public string title;
    [SerializeField] public Sprite icon;
    [SerializeField] public float cooldownTime = 10f;
    [SerializeField] public int maxCharges = 1;
    public ScCooldown cooldown;

    private int _charges;

    public virtual void Awake()
    {
        _charges = maxCharges;
    }

    public virtual void Try(ScEntity entity)
    {
        if (_charges > 0)
        {
            Activate(entity);
            //Debug.Log(_charges);
            //Debug.Log("Activated");
        }
        else
        {
            //Debug.Log("On CD Sound");
        }
    }

    protected virtual void Activate(ScEntity entity)
    {
        _charges--;
        if (_charges == maxCharges - 1)
        {
            StartCoroutine(Cooldown(entity));
        }
    }

    public virtual bool Cancel(ScEntity entity)
    {
        return true;
    }

    protected void StartCoroutine(IEnumerator coroutine)
    {
        CoroutineHandler.Instance.StartRoutine(coroutine);
    }

    protected void StopAllRoutine(IEnumerator coroutine)
    {
        CoroutineHandler.Instance.StopAllRoutine(coroutine);
    }

    protected IEnumerator Cooldown(ScEntity entity)
    {
        if (_charges < maxCharges)
        {
            cooldown.StartCooldown(cooldownTime / entity.Stats.cooldowns);
            yield return new WaitForSeconds(cooldownTime / entity.Stats.cooldowns);
            _charges++;
            //Debug.Log(_charges);
            StartCoroutine(Cooldown(entity));
        }
    }
}