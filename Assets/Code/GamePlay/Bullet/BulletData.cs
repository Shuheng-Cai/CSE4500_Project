// Module: BulletData
// Purpose: Create Bullet ScriptableObject
// Invariants: 
// Performance: 
// Dependencies: 
// Known Tricky Cases:
using UnityEngine;

[CreateAssetMenu(menuName = "Game/BulletData")]
public class BulletData : ScriptableObject
{
    public float lifeTime;
    public GameObject bulletPrefeb;
    public float damage;
    public float speed;
    public bool canPenetrate;
    public float shootInterval;
    public bool isLaser;
}
