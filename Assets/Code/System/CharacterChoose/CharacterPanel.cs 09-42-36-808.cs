using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharacterPanel : MonoBehaviour
{
    public Action<CharacterData> updatePanel;
    public TMP_Text attributeText;
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
        if (data == null) return;

        thisCharacter = data;

        if (attributeText != null) attributeText.gameObject.SetActive(true);
        if (characterImage != null) characterImage.gameObject.SetActive(true);
        if (bulletImage != null) bulletImage.gameObject.SetActive(true);

        if (attributeText != null)
        {
            attributeText.text =
                $"{data.BaseMaxHealthPoint}\n" +
                $"{data.BaseSpeed}\n" +
                $"{data.BaseStrength}";
        }

        if (characterImage != null)
            characterImage.sprite = data.CharacterImage;

        if (bulletImage != null)
            bulletImage.sprite = data.BulletImage;
    }

    public void ButtonClicked()
    {
        if (thisCharacter == null) return;
        if (PlayerManager.instance == null) return;
        if (GameManager.instance == null) return;

        PlayerManager.instance.ChangeCharacter(thisCharacter);
        GameManager.instance.EnterNextLevel();
    }
}