using System.Collections.Generic;
using UnityEngine;
using System;

public enum UpgradeType
{
    HP,
    Strength,
    Speed
}

public class PlayerUpgradeManage : MonoBehaviour
{
    public static PlayerUpgradeManage instance;
    public Dictionary<UpgradeType, Action> applyMap;

    public float upgradeCost;

    // Method
    void Awake()
    {
        applyMap = new Dictionary<UpgradeType, Action>
        {
            { UpgradeType.HP, () => {PlayerManager.instance.MaxHealth += 1f;  GameEvent.OnPlayerUpgradeUI.Invoke(UpgradeType.HP);}},
            { UpgradeType.Strength,  () => {PlayerManager.instance.Strength *= 1 + 0.01f;  GameEvent.OnPlayerUpgradeUI.Invoke(UpgradeType.Strength);} },
            { UpgradeType.Speed, () => {PlayerManager.instance.Speed *= 1 + 0.01f; GameEvent.OnPlayerUpgradeUI.Invoke(UpgradeType.Strength);}},
        };
    }
    void OnEnable()
    {
        GameEvent.OnPlayerUpgrade += OnPlayerUpgrade;
    }

    void OnDisable()
    {
        GameEvent.OnPlayerUpgrade -= OnPlayerUpgrade;
    }

    private void OnPlayerUpgrade()
    {
        ApplyUpgrade(ChooseUpgrade());
    }

    public void ApplyUpgrade(UpgradeType type)
    {
        if (GoldManager.instance.currentGold - upgradeCost >= 0)
        {
            applyMap[type].Invoke();
        }
    }

    public UpgradeType ChooseUpgrade()
    {
        Array values = Enum.GetValues(typeof(UpgradeType));
        return (UpgradeType)values.GetValue(UnityEngine.Random.Range(0, values.Length));
    }

    public void ResetUpgradeCost()
    {
        upgradeCost = 1;
    }
}
