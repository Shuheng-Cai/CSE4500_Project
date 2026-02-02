using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : BaseBullet
{
    protected override void Move()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
}
