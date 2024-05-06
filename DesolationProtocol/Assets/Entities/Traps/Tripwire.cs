using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripwireCollision : MonoBehaviour
{
    private bool canActivate = true;
    [SerializeField] private Spikewall SpikeWallTrap;

    private void OnTriggerEnter(Collider other)
    {
        if (SpikeWallTrap != null)
        {
            if (canActivate && other.gameObject.TryGetComponent<ScEntity>(out ScEntity EntityCollision))
            {
                SpikeWallTrap.RotateWall();
                StartCoroutine(ActivateTripwire());
            }
        }
    }
    private IEnumerator ActivateTripwire()
    {
        canActivate = false;
        Debug.Log("Tripwire activated");
        yield return new WaitForSeconds(3f);
        canActivate = true; Debug.Log("Tripwire Ready");
    }
}

