using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/*
    TODO: Automaticly Generate Player Choose page.
*/

public class CharacterChoosen : MonoBehaviour
{
    private CharacterData thisCharacter;
    Animator animator;

    void Awake()
    {
        thisCharacter = GetComponentInParent<MouseEnterButton>().characterData;
        animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = thisCharacter.CharacterAnimController;
    }

    public void ButtonClicked()
    {
        PlayerManager.instance.ChangeCharacter(thisCharacter);
        GameManager.instance.EnterNextLevel();
    }
}
