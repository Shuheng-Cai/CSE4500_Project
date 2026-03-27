using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharReturnMenu : MonoBehaviour
{
    public void ButtonClicked()
    {
        GameManager.instance.EnterMainMenu();
    }
}
