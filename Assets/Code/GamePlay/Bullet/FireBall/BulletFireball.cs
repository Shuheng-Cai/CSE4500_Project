using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFireball : BaseBullet
{
    protected override void Move()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            var enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                canMove = false;
                bulletAnim.animator.SetTrigger("isHit");
            }
        }
    }
}
