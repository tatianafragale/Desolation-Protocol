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
    [SerializeField] private LayerMask layerMask;

    //funcionamiento
    private bool shooting , reloading = false;
    public int bulletsLeft;
    private ScCooldown shootCd = new ScCooldown();
    
    
    public Camera fpsCam;
    public Transform atackPoint;
    private Animator _anim;

    private void Awake()
    {
        bulletsLeft = magazineSize;
        shootCd.ResetCooldown();
        _anim = GetComponent<Animator>();
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
        //bulletsLeft--;
        
         if (_anim != null)
        {
            _anim.SetTrigger("Fire");
        }

        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        Vector3 targetpoint;

        if (Physics.Raycast(ray,out RaycastHit hit, 100, layerMask))
        {
            targetpoint = hit.point;
        }
        else
        {
            targetpoint = ray.GetPoint(100);
        }
        Vector3 direction = atackPoint.position - targetpoint;

        Debug.DrawRay(targetpoint, targetpoint - atackPoint.position);
        GameObject currentBullet = Instantiate(bullet, atackPoint.position, Quaternion.LookRotation(targetpoint - atackPoint.position));
        currentBullet.GetComponent<ScProjectile>().owner = this.GetComponent<ScEntity>();

        if (bulletsLeft <= 0)
        {
            Reload();
            Invoke("Reload", reloadTime);
        }
    }

    public void Reload()
    {
        bulletsLeft = magazineSize;
    }
}
