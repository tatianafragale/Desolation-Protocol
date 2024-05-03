using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    [SerializeField] private Material _emissiveMaterial;
    [SerializeField] private Renderer _objectToChange;

    void Start()
    {
        _emissiveMaterial = _objectToChange.GetComponent<Renderer>().material;
    }

    void Update()
    {
        
    }

    public void TurnEmissionOff()
    {
        _emissiveMaterial.DisableKeyword("_EMISSION");
    }
    
    public void TurnEmissionOn()
    {
        _emissiveMaterial.EnableKeyword("_EMISSION");
    }
}
