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
        if (GameObject.FindWithTag("Player").transform.position != null)
        {
            target = GameObject.FindWithTag("Player").transform.position;
            FaceDir();
            Move();
        }

    }

    public void TakeDamage(float damage)
    {
        currentHealth = currentHealth - damage;
        InvulnerableAfterHit();
        animator.SetTrigger("isHit");
        if(currentHealth < 0)
        {
            Die();
        }
    }

    protected abstract void Move();

    // Hit player
    void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.transform.GetComponent<PlayerState>())
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

    IEnumerator InvulnerableAfterHit()
    {
        GetComponent<CapsuleCollider2D>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        GetComponent<CapsuleCollider2D>().enabled = true;
    }
}
