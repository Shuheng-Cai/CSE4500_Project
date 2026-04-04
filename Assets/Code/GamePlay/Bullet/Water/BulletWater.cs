using Unity.VisualScripting;
using UnityEngine;

public class BulletWater : BaseBullet
{
    protected override void Start()
    {
        
    }

    protected override void Move()
    {
        Vector2 mousePosition = Input.mousePosition;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 dir = new Vector2(mouseWorldPos.x - player.transform.position.x, mouseWorldPos.y - player.transform.position.y);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.position = player.transform.position;
        Quaternion bulletRotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = bulletRotation;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            var enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.speed = enemy.speed / 2;
            }
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            var enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            var enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.speed = enemy.speed * 2;
            }
        }
    }
}
