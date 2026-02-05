using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletAnim : MonoBehaviour
{
    public Animator animator;
    public BaseBullet bullet;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        bullet = GetComponentInParent<BaseBullet>();
    }

    protected virtual void OnHitEnemy()
    {
        bullet.OnHitEnemy();
    }
}
