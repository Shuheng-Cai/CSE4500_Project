using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Option : MonoBehaviour
{
    public void EnterBossLevel()
    {
        GameManager.instance.EnterBossLevel();
    }
}
