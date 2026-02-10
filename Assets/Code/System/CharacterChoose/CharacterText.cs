using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterText : MonoBehaviour
{
    public TMP_Text attributeText;
    public CharacterData characterData;

    void Start()
    {
        attributeText.GetComponent<TMP_Text>();
        Show(characterData);
    }

    public void Show(CharacterData data)
    {
        attributeText.text = 
            $"Speed:{data.BaseSpeed}\n" +
            $"HP:{data.BaseMaxHealthPoint}\n" +
            $"Strength:{data.BaseStrength}";
    }
}
