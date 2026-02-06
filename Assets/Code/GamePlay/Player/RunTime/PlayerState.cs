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

    public void TakeDamage(float damage)
    {
        if (!PlayerManager.instance.invulnerable)
        {
            currentHealthPoint -= damage;
            if(currentHealthPoint < 0)
            {
                PlayerManager.instance.Die();
            }
            StartCoroutine(PlayerManager.instance.SetPlayerInvulnerable());
        }
    }
}
