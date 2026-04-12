using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_BossHPBar : MonoBehaviour
{
    public Image HPBarFill;

    // Update is called once per frame
    private Boss targetBoss;

    void Update()
    {
        if (targetBoss == null)
        {
            targetBoss = FindObjectOfType<Boss>();
            return;
        }

        float curHP = targetBoss.currentHealth;
        float maxHP = targetBoss.maxBossHP;

        HPBarFill.fillAmount = curHP / maxHP;
    }
}
