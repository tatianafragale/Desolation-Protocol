using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

public class ScEntity : MonoBehaviour
{
    [SerializeField] private ScStatsLevel linkStats;
    public string entityName;
    public ScStats Stats;

    public float health = 100f;
    public int level = 0;
    public float experience = 0f;

    public string Team = "";

    public int totaljumps = 1;
    private int _jumps = 1;
    private float airControl = 0.5f;

    public Vector3 movement;
    public float Velocity;

    public bool landed = true;

    private Rigidbody _rigidbody;

    public Collider OnLandCollider;

    public ScAbilityHolder abilityHolder;

    private Animator _anim;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        abilityHolder = GetComponent<ScAbilityHolder>();

        _anim = GetComponent<Animator>();
    }

    void Start()
    {
        SetStats();
        _jumps = totaljumps;
        health = Stats.maxHealth;
    }

    private void SetStats()
    {
        Stats = linkStats.GetStats(entityName, level); //get stats per lvl
        //items modifiers
    }

    private void Update()
    {
        Velocity = new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z).magnitude;
    }

    private void FixedUpdate()
    {
        if (movement != Vector3.zero)
        {
            
            if (new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z).magnitude <= Stats.movementSpeed)
            {
                if (landed)
                {
                    _rigidbody.AddForce(Quaternion.LookRotation(_rigidbody.transform.forward, _rigidbody.transform.up) * movement * Stats.movementSpeed * 50, ForceMode.Acceleration);
                }
                else
                {
                    _rigidbody.AddForce(Quaternion.LookRotation(_rigidbody.transform.forward, _rigidbody.transform.up) * movement * Stats.movementSpeed * 50 * airControl, ForceMode.Acceleration);
                }
            }
            else
            {
                _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z).normalized * Stats.movementSpeed + Vector3.up * _rigidbody.velocity.y;
            }
        }


        //Animaciones de mover
        _anim.SetFloat("ZAxis", movement.z, 0.1f, Time.deltaTime); 
        _anim.SetFloat("XAxis", movement.x, 0.1f, Time.deltaTime); 

        //Regen
        if (health < Stats.maxHealth && Stats.regeneration > 0) Heal(Stats.regeneration * Time.fixedDeltaTime);
    }

    //Health
    public void TakeDamage(float incomingDamage, float incomingPenLinear = 0, float incomingPenPerc = 0)
    {
        //Deal Damage
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
            //morir
        }
    }

    public void Heal(float heal)
    {
        health += heal;
        if (health > Stats.maxHealth) health = Stats.maxHealth;
    }

    public void Jump(bool forced = false)
    {
        

        if (_jumps > 0 || forced)
        {
            _anim.SetTrigger("Jump");

            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z);
            _rigidbody.AddForce(_rigidbody.transform.up * Stats.jumpForce * 5f, ForceMode.VelocityChange);
            if (_jumps > 0)
            {
                _jumps--;
            }
        }
    }

    public void OnLand()
    {
        _jumps = totaljumps;
        landed = true;
        _anim.SetTrigger("Land");
        _rigidbody.drag = 0.01f;
    }

    public void OnAir()
    {
        _jumps = totaljumps - 1;
        landed = false;
        _rigidbody.drag = 0.1f;
    }
}