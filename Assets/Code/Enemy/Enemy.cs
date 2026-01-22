// ==================================================
// Module: Enemy
// Purpose: define the enemy: life and chase the target; take damage
// Author: Shuheng
// Date: 2025/10/11
// Dependencies: take damage by bullet; 
// ==================================================

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
    public float currentHealth;
    [HideInInspector] public float speed;
    [HideInInspector] public float damage;

    
    // Mathod
    protected virtual void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        damage = enemyData.damage;
        currentHealth = enemyData.maxHealth;
        speed = enemyData.speed;
        animator = transform.GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        target = GameObject.FindWithTag("Player").transform.position;
        FaceDir();
        Move();
    }

    public void TakeDamage(float damage)
    {
        currentHealth = currentHealth - damage;
        if(currentHealth < 0)
        {
            Die();
        }
    }

    protected abstract void Move();

    // Hit player
    void OisionEnter2D(Collision2D collision)
    {
        if (collision.transform.GetComponent<PlayerController>())
        {
            var player = collision.transform.GetComponent<PlayerState>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }

            OnHitEnemy();
        }
    }
 
    protected virtual void OnHitEnemy()
    {
        Destroy(gameObject);
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    protected virtual void FaceDir()
    {
        if(transform.position.x - target.x <= 0)
        {
            sprite.flipX = false;
        }
        
        else
        {
            sprite.flipX = true;
        }
    }
}
