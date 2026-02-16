using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRocket : BaseBullet
{

    public float explosionRadius = 3f;
    public float explosionDamageMultiplier = 0.7f;
    public GameObject explosionEffect;
    
    public GameObject rangeIndicator;
    
    protected override void Move()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy")) // TODO: collision with wall
        {
            Enemy hitEnemy = other.GetComponent<Enemy>();
            if (hitEnemy != null)
            {
                hitEnemy.TakeDamage(damage);
                canMove = false;
                
                if (bulletAnim != null) bulletAnim.animator.SetTrigger("isHit");
            }
            
            Explode(hitEnemy, transform.position);
        }
    }

    private void Explode(Enemy hitEnemy, Vector2 hitPoint)
    {
        
        if (rangeIndicator != null)
        {
            GameObject indicator = Instantiate(rangeIndicator, hitPoint, Quaternion.identity);
            AOERangeIndicator rangeScript = indicator.GetComponent<AOERangeIndicator>();
            if (rangeScript != null)
            {
                rangeScript.radius = explosionRadius;
            }
        }
        
        if (explosionEffect != null)
        {
            GameObject exp = Instantiate(explosionEffect, hitPoint, Quaternion.identity);
            
            RocketExplosion e = exp.GetComponent<RocketExplosion>();
            if (e != null)
            {
                e.explosionRadius = explosionRadius;
            }
            
        }

        Collider2D[] colliding_enemies = Physics2D.OverlapCircleAll(hitPoint, explosionRadius);

        foreach (Collider2D col in colliding_enemies)
        {
            Enemy enemy = col.GetComponent<Enemy>();
            if (enemy == null || enemy == hitEnemy) continue;

            float AOEDamage = damage * explosionDamageMultiplier;
            enemy.TakeDamage(AOEDamage);

        }
        
        Destroy(gameObject);
    }
}
