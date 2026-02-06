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

    public void Show(CharacterData data)
    {
        panel.gameObject.SetActive(true);
        attributeText.text = 
            $"Speed:{data.BaseSpeed}\n" +
            $"HP:{data.BaseMaxHealthPoint}\n" +
            $"Strength:{data.BaseStrength}";
    }

    public void Hide()
    {
        panel.gameObject.SetActive(false);
    }
}
