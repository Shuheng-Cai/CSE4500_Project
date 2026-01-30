// Module: Shooting
// Purpose: Player shoot.
// Invariants: 
// Performance: 
// Dependencies: Player State; Bullet
// Known Tricky Cases: 

using System.Collections;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    // TODO: Rewrite
    // Outlet
    public BulletData currentBulletData; // bulletPrefabs;
    public int bulletType;
    public Vector2 shootPosition; // record the shooting postion(Player transform)
    public bool canShoot; // whether player can shoot
    public Quaternion bulletRotation;

    // State Tracking
    private int faceDir;
    private float scalex;

    // Method
    public void MouseToPlayer()
    {
        Vector2 mousePosition = Input.mousePosition;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePosition);
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
    }
    
    public void Update()
    {
        shootPosition = gameObject.transform.position;
        MouseToPlayer();

        if (canShoot)
        {
            StartCoroutine(Shoot());
        }
    }

    // after spawn seconds, shoot once
    // TODO: FireRate for player
    IEnumerator Shoot()
    {
        canShoot = false;
        GameObject bullet = Instantiate(currentBulletData.bulletPrefeb, transform.position, bulletRotation);
        bullet.GetComponent<BaseBullet>().Init(currentBulletData);
        bullet.SetActive(true);
        yield return new WaitForSeconds(1f);
        canShoot = true;
    }
}
