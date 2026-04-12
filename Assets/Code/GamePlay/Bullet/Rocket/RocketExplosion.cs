using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketExplosion : MonoBehaviour
{
    public float explosionRadius = 2f;
    public float baseSpriteRadius = 0.005f;

    void Start()
    {
        float scale = explosionRadius / baseSpriteRadius;
        transform.localScale = new Vector3(scale, scale, 1f);
    }

    public void OnExplosion()
    {
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

}
