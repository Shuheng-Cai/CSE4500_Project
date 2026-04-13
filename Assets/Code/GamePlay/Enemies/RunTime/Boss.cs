using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    
    [Header("Attack 1: 360 Bullet")]
    public GameObject bulletPrefab;
    public int bulletCount360 = 36;
    public float bulletSpeed360 = 5f;
    public float bulletDamage360 = 10f;

    [Header("Attack 2: Charge and Jump")] 
    public GameObject attackIndicator;
    public float attackDelay;
    public float attackRadius;
    public float attackDamage;

    [Header("Attack 3: Fan Bullet")] 
    public float bulletDegree = 30;
    public int bulletCountFan = 36;
    public float bulletSpeedFan = 5f;
    public float bulletDamageFan = 10f;
    
    [Header("Attack 4: Burst")] 
    public float burstSpeed = 18f;
    public float burstDuration = 2f;
    public float burstDamage = 30f;
    public float burstDelay = 1.5f;
    
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
            
            //Randomly pick an attack
            if (Time.time >= nextAttackTime && bossState == BossState.Walking)
            {
                int choice = Random.Range(0, 4);
                switch (choice) {
                    case 0:
                        StartCoroutine(Attack_360Bullet());
                        break;
                    case 1:
                        StartCoroutine(Attack_ChargeJump());
                        break;
                    case 2:
                        StartCoroutine(Attack_FanBullet());
                        break;
                    case 3:
                        StartCoroutine(Attack_Burst());
                        break;
                }
            }
        }
    }
    
    protected override void Move() {
        Vector3 direction = (target - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
        animator.SetBool("isMove", true);
    }
    
    protected override void Die() {

        bossState = BossState.Dead;
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null) collider.enabled = false;
        
        StartCoroutine(BossDie());
    }
    
    private IEnumerator BossDie() {
        
        animator.SetBool("isMove", false);
        speed = 0;
        animator.SetTrigger("isDead");
        yield return new WaitForSeconds(1.9f);

        if (openDoor != null) {
            Vector3 doorPos = transform.position + Vector3.down * 2f;
            Instantiate(openDoor, doorPos, Quaternion.identity);
        }
        
        base.Die();
    }
    
    protected override void OnHitEnemy() {
        if (bossState != BossState.Walking) return;
        StartCoroutine(CollisionAttack());
    }
    
    private IEnumerator CollisionAttack() {

        bossState = BossState.Attacking;
        animator.SetBool("isMove", false);
        
        animator.SetTrigger("attack_collision");
        
        yield return new WaitForSeconds(1f);
        
        bossState = BossState.Walking;
        animator.SetBool("isMove", true);
        
    }
    
    //=======================================================================================
    
    private IEnumerator Attack_360Bullet()
    {
        bossState = BossState.Attacking;
        animator.SetBool("isMove", false);
        animator.SetTrigger("attack_360");

        yield return new WaitForSeconds(0.7f);

        float smallAngle = 360f / bulletCount360;
        for (int i = 0; i < bulletCount360; i++)
        {
            float angle = i * smallAngle * Mathf.Deg2Rad;
            Vector2 dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            SpawnBullet(dir, bulletSpeed360, bulletDamage360);
        }

        yield return new WaitForSeconds(0.5f);

        bossState = BossState.Walking;
        animator.SetBool("isMove", true);
        ScheduleNextAttack();
    }
    
    private void SpawnBullet(Vector2 direction, float spd, float dmg)
    {
        if (bulletPrefab == null) return;
        Vector3 pos = transform.position;
        pos.z = -1f; 

        GameObject bullet = Instantiate(bulletPrefab, pos, Quaternion.identity);
        BossBullet bb = bullet.GetComponent<BossBullet>();
        bb.Initialize(direction, spd, dmg);
    }

    //=======================================================================================

    private IEnumerator Attack_ChargeJump() {
        
        //Charge state
        bossState = BossState.Attacking;
        animator.SetBool("isMove", false);
        sprite.color = Color.red;

        Vector3 attackCenter = target;
        
        //Spawn attack indicator
        GameObject indicatorObject = Instantiate(attackIndicator, attackCenter, Quaternion.identity);
        AOERangeIndicator indicator = indicatorObject.GetComponent<AOERangeIndicator>();
        indicator.radius = attackRadius;
        indicator.autoFade = false;
        
        //Charge
        animator.SetTrigger("attack_charge");
        yield return new WaitForSeconds(attackDelay);
        if (bossState == BossState.Dead) {
            Destroy(indicatorObject); 
            yield break;
        }
        
        //Jump
        Vector3 pos = transform.position;
        float time = 0f;
        while (time < 0.5f) {
            time += Time.deltaTime;
            float p = time / 0.5f;
            transform.position = Vector3.Lerp(pos, pos + Vector3.up * 5f, p);
            sprite.color = new Color(1f, 0f, 0f, 1f - p * 0.7f);
            yield return null;
        }
        yield return new WaitForSeconds(0.2f);
        animator.SetTrigger("attack_aoe");
        
        //Attack
        
        Vector3 airPos = transform.position;
        time = 0f;
        while (time < 0.3f)
        {
            time += Time.deltaTime;
            float p = time / 0.3f;
            transform.position = Vector3.Lerp(airPos, attackCenter, p * p);
            sprite.color = new Color(1f, 0f, 0f, 0.3f + p * 0.7f);
            yield return null;
        }
        
        Destroy(indicatorObject);
        transform.position = attackCenter;
        
        PlayerState[] players = FindObjectsOfType<PlayerState>();
        foreach (PlayerState p in players)
        {
            if (Vector2.Distance(p.transform.position, attackCenter) <= attackRadius)
            {
                PlayerManager.instance.TakeDamage(attackDamage);
            }
        }

        sprite.color = Color.white;
        yield return new WaitForSeconds(0.4f);

        if (bossState == BossState.Dead) yield break;
        bossState = BossState.Walking;
        animator.SetBool("isMove", true);
        ScheduleNextAttack();

    }
    
    //=======================================================================================

    private IEnumerator Attack_FanBullet() {
        
        bossState = BossState.Attacking;
        animator.SetBool("isMove", false);
        animator.SetTrigger("attack_fan");

        yield return new WaitForSeconds(0.5f);
        
        Vector2 attackTarget = (target - transform.position).normalized;
        float baseAngle = Mathf.Atan2(attackTarget.y, attackTarget.x) * Mathf.Rad2Deg;
        
        float halfAngle = bulletDegree / 2f;
        float smallAngle = bulletDegree / (bulletCountFan - 1);
        for (int i = 0; i < bulletCountFan; i++)
        {
            float angle = (baseAngle - halfAngle + smallAngle * i) * Mathf.Deg2Rad;
            Vector2 dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            SpawnBullet(dir, bulletSpeedFan, bulletDamageFan);
        }

        yield return new WaitForSeconds(0.5f);

        bossState = BossState.Walking;
        animator.SetBool("isMove", true);
        ScheduleNextAttack();
    }
    
    //=======================================================================================

    private IEnumerator Attack_Burst() {
        bossState = BossState.Attacking;
        animator.SetBool("isMove", false);
        sprite.color = Color.red;
        Vector2 attackTarget = (target - transform.position).normalized;
        
        yield return new WaitForSeconds(burstDelay);
        sprite.color = Color.white;
        animator.SetBool("isBurst", true);
        
        float time = 0f;
        bool hasHit = false;
        while (time < burstDuration) {
            if (bossState == BossState.Dead) yield break;
            time += Time.deltaTime;
            
            transform.position += (Vector3)(attackTarget * burstSpeed * Time.deltaTime);
            
            if (!hasHit)
            {
                PlayerState[] players = FindObjectsOfType<PlayerState>();
                foreach (PlayerState p in players)
                {
                    if (Vector2.Distance(transform.position, p.transform.position) < 1.5f)
                    {
                        PlayerManager.instance.TakeDamage(burstDamage);
                        hasHit = true;
                        break;
                    }
                }
            }

            yield return null;
        }
        
        animator.SetBool("isBurst", false);
        animator.SetTrigger("EndBurst");
        yield return new WaitForSeconds(0.5f);

        if (bossState == BossState.Dead) yield break;
        bossState = BossState.Walking;
        animator.SetBool("isMove", true);
        ScheduleNextAttack();
    }
}
