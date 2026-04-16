using System.Collections.Generic;
using UnityEngine;
using System;

public enum AttributeUpgradeType
{
    HP,
    Strength,
    Speed
}

public class PlayerUpgradeManage : MonoBehaviour
{
    public static PlayerUpgradeManage instance;
    public Dictionary<AttributeUpgradeType, Action> applyMap;

    public float upgradeCost;

    // Method
    void Awake()
    {
        applyMap = new Dictionary<AttributeUpgradeType, Action>
        {
            { AttributeUpgradeType.HP, () => {PlayerManager.instance.MaxHealth += 1f; }},
            { AttributeUpgradeType.Strength,  () => {PlayerManager.instance.Strength *= 1 + 0.01f; } },
            { AttributeUpgradeType.Speed, () => {PlayerManager.instance.Speed *= 1 + 0.01f;}},
        };
    }
    void OnEnable()
    {
        Debug.Log("PlayerUpgradeManage OnEnable");
        GameEvent.OnPlayerUpgrade += OnPlayerUpgrade;
    }

    void OnDisable()
    {
        Debug.Log("PlayerUpgradeManage OnDisable");
        GameEvent.OnPlayerUpgrade -= OnPlayerUpgrade;
    }

    private void OnPlayerUpgrade()
    {
        ApplyUpgrade(ChooseUpgrade());
    }

    public void ApplyUpgrade(AttributeUpgradeType type)
    {
        if (GoldManager.instance.CostCoin(upgradeCost))
        {
            applyMap[type].Invoke();
            upgradeCost += 1;
        }
    }

    public AttributeUpgradeType ChooseUpgrade()
    {
        Array values = Enum.GetValues(typeof(AttributeUpgradeType));
        return (AttributeUpgradeType)values.GetValue(UnityEngine.Random.Range(0, values.Length));
    }

    public void ResetUpgradeCost()
    {
        upgradeCost = 1;
    }
}
