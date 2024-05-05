using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScCooldown
{
    private float nextTime = 0;
    public bool IsReady => Time.time >= nextTime;
    public void StartCooldown(float cooldownTime) => nextTime = Time.time + cooldownTime;
    public void ResetCooldown() => nextTime = Time.time;
    public void DecreaseCooldown(float decreaseTime) => nextTime -= decreaseTime;
    public float GetCooldown() => nextTime - Time.time;
}