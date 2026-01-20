using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    // Outlets
    public GameObject projectile;
    public float firingDelay;

    // Configuration

    // Method
    void Start()
    {
        StartCoroutine("Firing");
    }

    void FireProjectile(GameObject fireProjectile)
    {
        Instantiate(fireProjectile, transform.position, Quaternion.identity);
    }

    IEnumerator Firing()
    {
        yield return new WaitForSeconds(1f);

        FireProjectile(projectile);

        StartCoroutine("Firing");
    }
}
