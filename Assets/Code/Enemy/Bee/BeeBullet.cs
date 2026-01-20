using UnityEngine;

public class BeeBullet : MonoBehaviour
{
    // Outlets
    private Vector3 moveDirection;
    Rigidbody2D _rb;

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
        _rb = GetComponent<Rigidbody2D>();
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

            OnHitEnemy(collision);
        }
    }

    protected virtual void OnHitEnemy(Collider2D other)
    {
        Destroy(gameObject);
    }
}
