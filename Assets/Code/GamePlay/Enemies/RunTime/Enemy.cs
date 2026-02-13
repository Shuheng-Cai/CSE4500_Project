// ==================================================
// Module: Enemy
// Purpose: define the enemy: life and chase the target; take damage
// Author: Shuheng
// Date: 2025/10/11
// Dependencies: take damage by bullet; 
// ==================================================

using System.Collections;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    // Outlats
    public Vector3 target; 
    private SpriteRenderer sprite;
    public Animator animator;    
    public EnemyData enemyData;
    public Rigidbody2D _rb;

    // Configuration
    [HideInInspector] public float speed;   // Set in ScriptObject
    [HideInInspector] public float damage;

    // State Tracking
    public bool isMove;
    public float currentHealth;
    public bool invulnerable = false;

    // Mathod
    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        damage = enemyData.damage;
        currentHealth = enemyData.maxHealth;
        speed = enemyData.speed;
        animator = transform.GetComponent<Animator>();
        animator.SetBool("isMove", true);
    }

    protected virtual void Update()
    {
        if (PlayerManager.instance.playerAlive)
        {
            ChooseNearestPlayer();
            FaceDir();
            Move();
        }
    }

    public void TakeDamage(float damage)
    {
        if (!invulnerable)
        {
            currentHealth = currentHealth - damage;
            StartCoroutine(InvulnerableAfterHit());
            animator.SetTrigger("isHit");
            if(currentHealth < 0)
            {
                Die();
            }
        }

    }

    protected abstract void Move();

    // Hit player
    void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.transform.CompareTag("Player"))
        {
            PlayerManager.instance.TakeDamage(damage);

            OnHitEnemy();
        }
    }
 
    protected virtual void OnHitEnemy()
    {
        Destroy(gameObject);
    }

    // Die to Generate Coin, Hit player won't generate.
    protected virtual void Die()
    {
        Destroy(gameObject);
        GameEvent.OnDieEnemy?.Invoke(transform.position);
    }

    protected virtual void FaceDir()
    {
        if(transform.position.x - target.x <= 0)
        {
            sprite.flipX = true;
        }
        
        else
        {
            sprite.flipX = false;
        }
    }

    protected virtual void ChooseNearestPlayer()
    {
        PlayerState[] players = FindObjectsOfType<PlayerState>();
        float closestTarget = 9999f;

        for(int i = 0; i < players.Length; i++)
        {
            PlayerState player = players[i];

            Vector2 directionToTarget = transform.position - player.transform.position;

            if(directionToTarget.sqrMagnitude < closestTarget)
            {
                closestTarget = directionToTarget.sqrMagnitude;

                target = player.transform.position;
            }
        }
    }

    IEnumerator InvulnerableAfterHit()
    {
        invulnerable = true;
        yield return new WaitForSeconds(0.5f);
        invulnerable = false;
    }
}
