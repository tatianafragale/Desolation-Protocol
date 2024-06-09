using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ScEntity : MonoBehaviour
{
    [SerializeField] private ScStatsLevel linkStats;
    public string entityName;
    public string Team = "";
    public ScStats Stats;

    public float health = 100f;
    public int level = 0;

    protected Rigidbody _rigidbody;
    protected Animator _anim;
    protected AudioSource _audioSource;

    [SerializeField] protected ScAbility[] abilities;

    //effects
    public int silencers = 0;

    public UnityEvent OnTakingDmg;
    public UnityEvent OnHeal;
    public UnityEvent OnDeath;

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        foreach (ScAbility _ability in abilities)
        {
            if (_ability)
            {
                _ability.cooldown.ResetCooldown();
            }
        }
    }

    protected virtual void Start()
    {
        SetStats();
        health = Stats.maxHealth;
    }

    private void SetStats()
    {
        Stats = linkStats.GetStats(entityName, level); //get stats per lvl
        //items modifiers
    }

    protected virtual void Update()
    {
        //update
    }

    protected virtual void FixedUpdate()
    {
        //Regen
        if (0 < health && health < Stats.maxHealth && Stats.regeneration != 0) Heal(Stats.regeneration * Time.fixedDeltaTime);
    }

    //Health
    public virtual void TakeDamage(float incomingDamage, float incomingPenLinear = 0, float incomingPenPerc = 0)
    {
        if (health > 0)
        {
            OnTakingDmg.Invoke();
            float finalArmor = (Stats.armor * (1 - incomingPenPerc) - incomingPenLinear); //Define Final Armor
            if (finalArmor >= 0)
            {
                health -= incomingDamage * (100f / (100f + finalArmor));
            }
            else
            {
                health -= incomingDamage * (2 - (100f / (100f - finalArmor)));
            }
            //Validate Death
            if (health <= 0)
            {
                Die();
            }
        }
    }

    protected virtual void Die()
    {
        OnDeath.Invoke();
        _anim.SetTrigger("Death");
        if (Team == "Enemy" && entityName != "Explosive")
        {
            Destroy(gameObject);
        }
    }

    public virtual void Heal(float heal)
    {
        OnHeal.Invoke();
        health += heal;
        if (health > Stats.maxHealth) health = Stats.maxHealth;
    }

    public virtual void TryAbility(int _selected)
    {
        if (silencers == 0)
        {
            if (_selected < abilities.Length)
            {
                if (abilities[_selected])
                {
                    abilities[_selected].Try(this);
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
        else
        {
            print("Silenciado");
        }
    }
}