using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static Cinemachine.DocumentationSortingAttribute;

public class ScStatsLevel : MonoBehaviour
{
    [SerializeField] public NewStatsLevel[] allStats;

    public ScStats GetStats(string title, int level = 0)
    {
        ScStats returnStats = new ScStats();
        foreach (NewStatsLevel stat in allStats)
        {
            if (stat.title == title)
            {
                returnStats = new ScStats
                {
                    maxHealth = stat.statsBase.maxHealth + stat.statsIncremental.maxHealth * level,
                    regeneration = stat.statsBase.regeneration + stat.statsIncremental.regeneration * level,
                    armor = stat.statsBase.armor + stat.statsIncremental.armor * level,
                    damage = stat.statsBase.damage + stat.statsIncremental.damage * level,
                    attackSpeed = stat.statsBase.attackSpeed + stat.statsIncremental.attackSpeed * level,
                    movementSpeed = stat.statsBase.movementSpeed + stat.statsIncremental.movementSpeed * level,
                    jumpForce = stat.statsBase.jumpForce + stat.statsIncremental.jumpForce * level,
                    critPercent = stat.statsBase.critPercent + stat.statsIncremental.critPercent * level,
                    critMultiplier = stat.statsBase.critMultiplier + stat.statsIncremental.critMultiplier * level,
                    cooldowns = stat.statsBase.cooldowns + stat.statsIncremental.cooldowns * level
                };
                return returnStats;
            }
        }
        return returnStats;
    }
}

[Serializable]
public class NewStatsLevel
{
    [SerializeField] public string title;
    [SerializeField] public ScStats statsBase;
    [SerializeField] public ScStats statsIncremental;
}
