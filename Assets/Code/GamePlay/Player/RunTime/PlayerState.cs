using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    // Outlets
    public static PlayerState instance;

    // Configuration
    public float currentHealthPoint;

    // Method
    void Awake()
    {
        currentHealthPoint = PlayerManager.instance.MaxHealth;
    }

    void Start()
    {
        instance = this;
    }

    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        currentHealthPoint -= damage;
        if(currentHealthPoint < 0)
        {
            Die();
        }
    }

    private void Die()
    {
        GameManager.instance.EnterMainMenu();
        Destroy(gameObject);
    }
}
