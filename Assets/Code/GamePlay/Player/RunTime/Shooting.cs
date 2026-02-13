// Module: Shooting
// Purpose: Player shoot.
// Invariants: 
// Performance: 
// Dependencies: Player State; Bullet
// Known Tricky Cases: 

using System.Collections;
using System.Linq;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    // Outlet
    public Vector2 shootPosition; // record the shooting postion(Player transform)
    public Quaternion bulletRotation;
    public Vector3 mouseWorldPos;

    // State Tracking
    SpriteRenderer sr;

    // Method
    void OnEnable()
    {
        GameEvent.ShootEachBattleLevel += FirstTimeEachLevel;
    }

    void OnDisable()
    {
        GameEvent.ShootEachBattleLevel -= FirstTimeEachLevel;
    }

    public void MouseToPlayer()
    {
        Vector2 mousePosition = Input.mousePosition;
        mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePosition);
        mouseWorldPos.z = 0f;
        Vector3 direction = mouseWorldPos - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bulletRotation = Quaternion.Euler(0, 0, angle);

        sr.flipX = direction.x < 0;
    }

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        MouseToPlayer();
    }
    
    public void Update()
    {
        shootPosition = gameObject.transform.position;
        MouseToPlayer();
    }

    public void FirstTimeEachLevel()
    {
        if (PlayerManager.instance.canShoot)
        {
            StartCoroutine(Shoot());
        }

        foreach(var i in PlayerManager.instance.currentLasers)
        {
            GameObject bullet = Instantiate(i.bulletPrefeb, transform.position, bulletRotation);
            bullet.GetComponent<BaseBullet>().Init(i, mouseWorldPos);
        }
        
        PlayerManager.instance.EnterBattle();
    }

    // after spawn seconds, shoot once
    // TODO: FireRate for player
    IEnumerator Shoot()
    {
        foreach(var currentBulletData in PlayerManager.instance.currentNormalBullets)
        {
            GameObject bullet = Instantiate(currentBulletData.bulletPrefeb, transform.position, bulletRotation);
            bullet.GetComponent<BaseBullet>().Init(currentBulletData, mouseWorldPos);
        }
        yield return new WaitForSeconds(1f);

        if(PlayerManager.instance.canShoot)
            StartCoroutine(Shoot());
    }
}
