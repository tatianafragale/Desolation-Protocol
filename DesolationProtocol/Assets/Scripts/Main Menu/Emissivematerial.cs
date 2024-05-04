using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emissivematerial : MonoBehaviour
{

    private Material _emissiveMat;
    [SerializeField] private float _value;
    private Color _color = Color.yellow;

    [SerializeField] private float _regulator = 1.0f;

    [SerializeField] private float _minEmissionValue = -0.8f;
    [SerializeField] private float _maxEmissionValue = 0.5f;
    private bool _emissionRising = true;


    // Start is called before the first frame update
    void Start()
    {
        Renderer rend = GetComponent<Renderer>();

        _emissiveMat = rend.GetComponent<Renderer>().material;
    }

    void Update()
    {

        if (_emissionRising)
        {
            _value += Time.deltaTime * _regulator;
            _emissiveMat.SetColor("_EmissionColor", _color * _value);

            if (_value >= _maxEmissionValue)
            {
                _emissionRising = false;
            }
            
        }
        else
        {
            _value -= Time.deltaTime * _regulator;
            _emissiveMat.SetColor("_EmissionColor", _color * _value);

            if (_value <= _minEmissionValue)
            {
                _emissionRising = true;
            }
        }
    }


}
