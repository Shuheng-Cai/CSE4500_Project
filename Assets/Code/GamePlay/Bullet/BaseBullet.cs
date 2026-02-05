// Module: BaseBullet
// Purpose: Define the father type of Bullet.
// Invariants: 
// Performance: 
// Dependencies: 
// Known Tricky Cases: canPenetrate; fireRate is from Player state, which is not from the bullet.

using UnityEngine;
using UnityEngine.UIElements;

public abstract class BaseBullet : MonoBehaviour
{
    // Outlet
    public BulletAnim bulletAnim;

    [HideInInspector]public float damage;
    [HideInInspector]public float speed;
    public GameObject player;
    [HideInInspector]public float lifeTime = 3f;
    [HideInInspector]public bool canPenetrate = false;
    [HideInInspector]public bool canMove = true;
    [HideInInspector]public Vector3 mousePosition;

    public void Init(BulletData bulletData, Vector3 mouseWorldPos)
    {
        this.speed = bulletData.speed;
        this.lifeTime = bulletData.lifeTime;
        this.canPenetrate = bulletData.canPenetrate;
        this.damage = bulletData.damage;
        mousePosition = mouseWorldPos;
    }

    // TODO: Damage.
    public void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        damage = 100;
        bulletAnim = GetComponentInChildren<BulletAnim>();
    }

    protected virtual void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    protected virtual void Update()
    {
        if(canMove)
            Move();
    }

    protected abstract void Move(); // realize movement in the child.

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            var enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            OnHitEnemy();
        }
    }

    public virtual void OnHitEnemy()
    {
        if(!canPenetrate)
            Destroy(gameObject);
    }
}
