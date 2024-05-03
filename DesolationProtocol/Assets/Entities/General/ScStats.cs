using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScStats
{
    public float maxHealth = 100f;          //Max Health
    public float regeneration = 0f;         //Heal per sec
    public float armor = 10f;               //Reduce incoming damage
    public float damage = 10f;              //Damage
    public float attackSpeed = 1f;          //Shoots per sec
    public float movementSpeed = 12f;        //Movement Speed
    public float jumpForce = 1f;            //Jump Force
    public float critPercent = 1f;          //% of Dealing Critical Hits
    public float critMultiplier = 1.2f;     //Multiplier of Critical Hits
    public float cooldowns = 1f;            //Reduce Time of Cooldowns
}
