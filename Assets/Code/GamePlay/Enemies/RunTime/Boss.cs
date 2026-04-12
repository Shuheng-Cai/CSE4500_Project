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
    
    [Header("Attack1: 360 Bullet")]
    public GameObject bulletPrefab;
    public int bulletCount360 = 36;
    public float bulletSpeed360 = 5f;
    public float bulletDamage360 = 10f;

    [Header("Attack2: Charge and Jump")] 
    public GameObject attackIndicator;
    public float attackDelay;
    public float attackRadius;
    public float attackDamage;
    
    
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
                int choice = Random.Range(0, 2);
                switch (choice)
                {
                    case 0: StartCoroutine(Attack_360Bullet()); break;
                    case 1: StartCoroutine(Attack_Charge_Jump()); break;
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
    
    
    
    
    
    
    
    
    
    
    
    //Cool Attacks
    
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
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        BossBullet bb = bullet.GetComponent<BossBullet>();
        bb.Initialize(direction, spd, dmg);
    }














    private IEnumerator Attack_Charge_Jump() {
        
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

        time = 0f;
        while (time < 0.15f)
        {
            time += Time.deltaTime;
            yield return null;
        }

        sprite.color = Color.white;
        yield return new WaitForSeconds(0.4f);

        if (bossState == BossState.Dead) yield break;
        bossState = BossState.Walking;
        animator.SetBool("isMove", true);
        ScheduleNextAttack();

    }
}
