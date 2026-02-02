using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharacterPanel : MonoBehaviour
{
    public RectTransform panel;
    public TMP_Text attributeText;
    public Vector2 panelToButtonOffset;

    void Awake()
    {
        panel = transform.GetComponent<RectTransform>();
        Hide();
    }

    public void Show(CharacterData data, Vector2 screenPos)
    {
        panel.gameObject.SetActive(true);
        panel.anchoredPosition = screenPos + panelToButtonOffset;
        attributeText.text = 
            $"Speed:{data.BaseSpeed}\n" +
            $"HP:{data.BaseMaxHealthPoint}\n" +
            $"Damage:{data.BaseDamage}";
    }

    public void Hide()
    {
        panel.gameObject.SetActive(false);
    }
}
