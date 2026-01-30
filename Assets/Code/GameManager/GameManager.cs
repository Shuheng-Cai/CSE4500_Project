using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    // Outlet
    public static int runGameSeed { get; private set; }
    public static GameManager instance {get; private set;}

    // Method
    void Awake()
    {
        instance = this;
        GameSeedInit();
        DontDestroyOnLoad(gameObject);
    }

    public static void GameSeedInit()
    {
        runGameSeed = (int)DateTime.UtcNow.Ticks;
    }
}
