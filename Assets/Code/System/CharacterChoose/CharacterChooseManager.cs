using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterChooseManager : MonoBehaviour
{
    public List<CharacterPanel> CharacterPanelList;
    public List<CharacterData> CharacterList;
    public Action SetPanel;
    private List<CharacterData> currentCharacterList = new List<CharacterData>();
    
    // CharacterPoint
    private int characterPointerLeft;
    public int characterPointerRight;


    void Start()
    {
        characterPointerLeft = 0;
        characterPointerRight = Mathf.Min(2, CharacterList.Count - 1);
        SetPage();
    }

    void Update()
    {
        
    }

    // TODO: Use in page scene
    public void NextPage()
    {
        
    }
    
    public void PrevPage()
    {
        
    }

    public void SetPage()
    {
        for(int i = characterPointerLeft; i <= characterPointerRight; i++)
        {
            currentCharacterList.Add(CharacterList[i]);
        }

        for(int i = 0; i < Mathf.Min(CharacterPanelList.Count, 2); i++)
        {
            CharacterPanel characterPanel = CharacterPanelList[i];
            characterPanel.GetComponent<CharacterChoosen>().thisCharacter = currentCharacterList[i];
            characterPanel.updatePanel.Invoke(currentCharacterList[i]);
        }
    }
}
