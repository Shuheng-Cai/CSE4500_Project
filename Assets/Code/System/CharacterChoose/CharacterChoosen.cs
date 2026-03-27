using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/*
    TODO: Automaticly Generate Player Choose page.
*/

public class CharacterChoosen : MonoBehaviour
{
    public CharacterData thisCharacter;

    public void ButtonClicked()
    {
        PlayerManager.instance.ChangeCharacter(thisCharacter);
        GameManager.instance.EnterNextLevel();
    }
}
