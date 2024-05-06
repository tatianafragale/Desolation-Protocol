using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Daño : MonoBehaviour
{
   private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<ScEntity>(out ScEntity EntityCollision))
        {
            EntityCollision.TakeDamage(10);
        }
    }
}
