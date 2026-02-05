using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTest : MonoBehaviour
{
   // Outlet
    public int bulletType;
    public Vector2 shootPosition; // record the shooting postion(Player transform)
    public bool canShoot; // whether player can shoot
    public Quaternion bulletRotation;
    public Vector3 mouseWorldPos;
    public BulletData currentBulletData;

    // State Tracking
    private int faceDir;
    private float scalex;

    // Method
    public void MouseToPlayer()
    {
        Vector2 mousePosition = Input.mousePosition;
        mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePosition);
        mouseWorldPos.z = 0f;
        Vector3 direction = mouseWorldPos - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        bulletRotation = Quaternion.Euler(0, 0, angle);

        if (faceDir * direction.x < 0)
        {
            faceDir = -faceDir;
            transform.localScale = new Vector3(faceDir * scalex, transform.localScale.y);
        }
    }

    void Start()
    {
        scalex = transform.localScale.x;
        canShoot = true;
        bulletType = 0;
        faceDir = 1;

        ShootLaser(currentBulletData);
    }
    
    public void Update()
    {
        shootPosition = gameObject.transform.position;
        MouseToPlayer();

        if (canShoot)
        {
            //StartCoroutine(Shoot());
        }
    }

    // after spawn seconds, shoot once
    // TODO: FireRate for player
    IEnumerator Shoot()
    {
        canShoot = false;
        GameObject bullet = Instantiate(currentBulletData.bulletPrefeb, transform.position, bulletRotation);
        bullet.GetComponent<BaseBullet>().Init(currentBulletData, mouseWorldPos);
        bullet.SetActive(true);
        yield return new WaitForSeconds(currentBulletData.shootInterval);
        canShoot = true;
    }

    public void ShootLaser(BulletData currentBullet)
    {
        if (currentBullet.isLaser)
        {
            GameObject bullet = Instantiate(currentBulletData.bulletPrefeb, transform.position, bulletRotation);
            bullet.GetComponent<BaseBullet>().Init(currentBulletData, mouseWorldPos);
        }
    }
}
