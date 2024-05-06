using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScGM : MonoBehaviour
{
    [SerializeField] private int _Ammo;
    [SerializeField] private int _AmmoMax;
    [SerializeField] private bool IsFiring;
    public Text AmmoCounter;
    
    public Text WaveCounter;
    [SerializeField] private int _Wave;
    [SerializeField] private Slider HpBar;
    [SerializeField] private ScEntity _Entity;


    void Start()
    {
        _Ammo = _AmmoMax;
        _Wave = 1;
    }
    private void Awake()
    {
         CountHP();
    }

    void Update()
    {
        AmmoCounter.text = _Ammo.ToString();
        WaveCounter.text = "Oleada " + _Wave.ToString();
        /*if (Input.GetMouseButtonDown(0) && !IsFiring && _Ammo > 0)
        {
            IsFiring = true;
            _Ammo--;
            IsFiring=false;
        }*/
    }

    public void WaveIncrease()
    {
        _Wave++;
    }
    public void CountHP()
    {
        HpBar.value = _Entity.health / _Entity.Stats.maxHealth;
    }
}
