using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScWeapon : MonoBehaviour
{
    //variables armas
    public GameObject bullet;
    [SerializeField] private int magazineSize = 30;
    [SerializeField] private float shootTime = 0.2f;
    [SerializeField] private float reloadTime = 3f;
    [SerializeField] private bool automatic;

    //funcionamiento
    private bool shooting , reloading = false;
    public int bulletsLeft;
    private ScCooldown shootCd = new ScCooldown();
    
    
    public Camera fpsCam;
    public Transform atackPoint;

    private void Awake()
    {
        bulletsLeft = magazineSize;
        shootCd.ResetCooldown();
    }

    private void Start()
    {
        Invoke("WaitForDestroy", 5f);
    }

    private void TryShoot()
    {
        if (shootCd.IsReady && shooting && !reloading && bulletsLeft > 0)
        {
            Shoot();
        }
    }

    public void SetShooting(bool value)
    {
        shooting = value;
        if (shooting)
        {
            TryShoot();
        }
        
    }

    private void Shoot()
    {
        shootCd.StartCooldown(shootTime);
        Invoke("TryShoot", shootTime);
        //bulletsLeft--;                                                    ACTIVAR SACAR BALAS
        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 targetpoint;
        if(Physics.Raycast(ray,out hit))
        {
            targetpoint = hit.point;
        }
        else
        {
            targetpoint = ray.GetPoint(100);
        }
        Vector3 direction = targetpoint - atackPoint.position;

        GameObject currentBullet = Instantiate(bullet, atackPoint.position, Quaternion.identity);
        currentBullet.transform.up = direction.normalized;
        currentBullet.GetComponent<ScProjectile>().owner = this.GetComponent<ScEntity>();

    }

    public void Reload()
    {

    }

    public void WaitForDestroy()
    {
        Destroy(gameObject);
    }
}
