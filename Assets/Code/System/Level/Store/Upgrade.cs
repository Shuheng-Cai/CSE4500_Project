using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    private float upgradeCost = 5f;
    public void HealthUpgrade()
    {
        if(GoldManager.instance.CostCoin(upgradeCost))
        {
            PlayerManager.instance.MaxHealth += 20;
            PlayerManager.instance.currentHealth = PlayerManager.instance.MaxHealth;
        }
    }

    public void SpeedUpgrade()
    {
        if(GoldManager.instance.CostCoin(upgradeCost))
        {
            PlayerManager.instance.Speed += 0.5f;
        }
    }

    public void DamageUpgrade()
    {
        if(GoldManager.instance.CostCoin(upgradeCost))
        {
            PlayerManager.instance.Strength += 0.5f;
        }
    }
}
