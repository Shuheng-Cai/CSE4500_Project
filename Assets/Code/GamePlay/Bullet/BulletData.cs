using UnityEngine;

public enum BulletType
{
    Normal,     // stright and hit
    Laser,      // only trigger once when player generate
}
[CreateAssetMenu(menuName = "Game/BulletData")]
public class BulletData : ScriptableObject
{
    public BulletType bulletType;
    public float lifeTime;
    public GameObject bulletPrefeb;
    public float damage;
    public float speed;
    public float shootInterval;
    public bool isLaser;
}
