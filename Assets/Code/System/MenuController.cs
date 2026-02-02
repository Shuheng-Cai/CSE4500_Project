using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public void StartGame()
    {
        GameManager.instance.StartGame();
    }

    public void ChooseCharacter()
    {
        GameManager.instance.CharacterChangePage();
    }
}
