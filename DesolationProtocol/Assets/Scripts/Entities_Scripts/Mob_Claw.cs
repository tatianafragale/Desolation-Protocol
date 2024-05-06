using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_Claw : MonoBehaviour
{

    [SerializeField] private int _maxAttackDMG;
    [SerializeField] private int _minAttackDMG;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.TryGetComponent<ScEntity>(out ScEntity Entity))
        {

            if (Entity.Team == "Player")
            {
                Entity.TakeDamage(Random.Range(_minAttackDMG, _maxAttackDMG));
            }
        }
    }
}
