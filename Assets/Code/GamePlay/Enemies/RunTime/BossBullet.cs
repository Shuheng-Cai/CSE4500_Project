using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour {
    // Configuration
    public float lifeTime;
    public float speed;
    private float damage;
    private Vector2 direction;

    public void Initialize(Vector2 dir, float spd, float dmg) {
        direction = dir.normalized;
        speed = spd;
        damage = dmg;
    }

    void Start() {
        Destroy(gameObject, lifeTime);
    }

    void Update() {
        Move();
    }

    private void Move() {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            PlayerManager.instance.TakeDamage(damage);

            Destroy(gameObject);
        }
    }
}
