using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    // Outlets
    public static PlayerState instance;

    // Configuration
    public float maxHealthPoint;
    private float currentHealthPoint;

    // Method
    void Start()
    {
        instance = this;
    }

    void Update()
    {
        Died();
    }

    public void TakeDamage(float damage)
    {
        currentHealthPoint -= damage;
    }

    private void Died()
    {
        Destroy(gameObject);
    }
}
