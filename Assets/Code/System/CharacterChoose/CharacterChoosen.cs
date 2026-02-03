using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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
        PlayerManager.instance.currentCharacter = thisCharacter;
        Debug.Log(PlayerManager.instance.currentCharacter.ToString());
        GameManager.instance.EnterNextLevel();
    }
}
