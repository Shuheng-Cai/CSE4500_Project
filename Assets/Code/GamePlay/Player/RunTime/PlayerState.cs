using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    // Outlets
    public static PlayerState instance;

    void Start()
    {
        instance = this;
    }

    public void TakeDamage(float damage)
    {
        if (!PlayerManager.instance.invulnerable)
        {
            PlayerManager.instance.currentHealth -= damage;
            if(PlayerManager.instance.currentHealth < 0)
            {
                PlayerManager.instance.Die();
            }
            StartCoroutine(PlayerManager.instance.SetPlayerInvulnerable());
        }
    }
}
