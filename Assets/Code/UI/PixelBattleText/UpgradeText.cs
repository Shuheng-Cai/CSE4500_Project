using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelBattleText;
using UnityEngine.UI;

public class UpgradeText : MonoBehaviour
{
    public TextAnimation textAnimation;
    public Canvas canvas;
    public Camera camera1;

    // Method
    void OnEnable()
    {
        GameEvent.OnPlayerUpgradeUI += UpgradeUI;
    }

    void OnDisable()
    {
        GameEvent.OnPlayerUpgradeUI -= UpgradeUI;
    }

    public void UpgradeUI(UpgradeType upgradeType)
    {
        var text = upgradeType.ToString();

        PixelBattleTextController.DisplayText(text, textAnimation, Vector3.zero);
    }
}
