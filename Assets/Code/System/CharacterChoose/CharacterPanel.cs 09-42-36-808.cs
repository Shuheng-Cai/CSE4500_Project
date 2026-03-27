using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class CharacterPanel : MonoBehaviour
{
    public Action<CharacterData> updatePanel;
    public TMP_Text attributeText;
    public TMP_Text characterName;
    public Image characterImage;
    public Image bulletImage;
    private CharacterData thisCharacter;

    void OnEnable()
    {
        updatePanel += UpdatePanel;
    }

    void OnDisable()
    {
        updatePanel -= UpdatePanel;
    }

    public void UpdatePanel(CharacterData data)
    {
        thisCharacter = data;
        attributeText.gameObject.SetActive(true);
        characterImage.gameObject.SetActive(true);
        bulletImage.gameObject.SetActive(true);
        characterName.gameObject.SetActive(true);
        
        attributeText.text = 
            $"{data.BaseMaxHealthPoint}\n" +
            $"{data.BaseSpeed}\n" +
            $"{data.BaseStrength}";

        characterImage.sprite = data.CharacterImage;
        bulletImage.sprite = data.BulletImage;
        characterName.text = data.Name;   
    }

    public void ButtonClicked()
    {
        if(thisCharacter == null) return;
        PlayerManager.instance.ChangeCharacter(thisCharacter);
        GameManager.instance.EnterNextLevel();
    }
}
