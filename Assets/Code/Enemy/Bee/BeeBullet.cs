using UnityEngine;

public class BeeBullet : MonoBehaviour
{
    // Outlets

    // Configuration
    public float lifeTime;
    public int speed;
    private float damage;

    // Method
    public void setDamage(float damage)
    {
        this.damage = damage;
    }

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var player = collision.GetComponent<PlayerState>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
    }
}
