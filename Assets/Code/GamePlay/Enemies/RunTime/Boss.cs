using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    
    public GameObject openDoor;
    
    public float maxBossHP;
    
    public float minAttackInterval = 5f;
    public float maxAttackInterval = 8f;
    
    private enum BossState { Walking, Attacking, Dead }
    private BossState bossState = BossState.Walking;
    private float nextAttackTime;
    
    [Header("Attack_360")]
    public GameObject bulletPrefab;
    public int bulletCount360 = 36;
    public float bulletSpeed360 = 5f;
    public float bulletDamage360 = 10f;
    
    protected override void Awake()
    {
        base.Awake();
        maxBossHP = currentHealth;
        ScheduleNextAttack();
    }
    
    private void ScheduleNextAttack()
    {
        nextAttackTime = Time.time + Random.Range(minAttackInterval, maxAttackInterval);
    }

    protected override void Update() {
        if (!PlayerManager.instance.playerAlive) return;
        ChooseNearestPlayer();
        FaceDir();
        
        if (bossState == BossState.Walking)
        {
            Move();

            if (Time.time >= nextAttackTime && bossState == BossState.Walking)
            {
                StartCoroutine(Attack_360Bullet());
            }
        }
    }
    
    protected override void Move() {
        Vector3 direction = (target - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
        animator.SetBool("isMove", true);
    }
    
    protected override void Die()
    {
        if (openDoor != null) {
            Instantiate(openDoor, transform.position, Quaternion.identity);
        }

        base.Die();
    }
    
    protected override void OnHitEnemy()
    {
        if (bossState != BossState.Walking) return;
        StartCoroutine(CollisionAttack());
    }
    
    private IEnumerator CollisionAttack()
    {

        bossState = BossState.Attacking;
        animator.SetBool("isMove", false);
        
        animator.SetTrigger("attack_collision");
        
        yield return new WaitForSeconds(1f);
        
        bossState = BossState.Walking;
        animator.SetBool("isMove", true);
        
    }
    
    //Cool Attacks
    
    private IEnumerator Attack_360Bullet()
    {
        bossState = BossState.Attacking;
        animator.SetBool("isMove", false);
        animator.SetTrigger("attack_360");

        yield return new WaitForSeconds(0.7f);

        float angleStep = 360f / bulletCount360;
        for (int i = 0; i < bulletCount360; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            Vector2 dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            SpawnBullet(dir);
        }

        yield return new WaitForSeconds(0.5f);

        bossState = BossState.Walking;
        animator.SetBool("isMove", true);
        ScheduleNextAttack();
    }
    
    private void SpawnBullet(Vector2 direction)
    {
        if (bulletPrefab == null) return;
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        BossBullet bb = bullet.GetComponent<BossBullet>();
        if (bb != null)
        {
            bb.Initialize(direction, bulletSpeed360, bulletDamage360);
        }
    }
}
