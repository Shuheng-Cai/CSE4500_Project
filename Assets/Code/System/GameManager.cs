using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Security.Cryptography;
using UnityEngine.SocialPlatforms;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;

public enum GameState
{
    MainMenu,
    Battle,
    Maze,
    Paused,
    GameOver,
    Character
}

public class GameManager : MonoBehaviour
{
    // Outlet
    public static int runGameSeed { get; private set; }
    public static GameManager instance {get; private set;}
    public static float totalGold {get; private set;}
    
    // State Tracking
    public GameState currentState {get; private set;}

    // Method
    void Awake()
    {
        instance = this;
        GameSeedInit();
        DontDestroyOnLoad(gameObject);
        currentState = GameState.MainMenu;
        EnterMainMenu();
    }

    // When Enter the main menu
    public void EnterMainMenu()
    {
        currentState = GameState.MainMenu;
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }

    // When Pause the game, only play stage
    public void PauseGame()
    {
        if(currentState != GameState.Battle || currentState != GameState.Maze ) return;
        currentState = GameState.Paused;
        Time.timeScale = 0f;
    }

    // TODO: How to decide the scene
    public void GameSeedInit()
    {
        runGameSeed = (int)DateTime.UtcNow.Ticks;
    }

    public void StartGame()
    {
        currentState = GameState.Battle;
        PlayerManager.instance.PlayerGenerate();
        SceneManager.LoadScene("Battle");
    }

    public void CharacterChangePage()
    {
        currentState = GameState.Character;
        SceneManager.LoadScene("CharacterChoose");
    }
}
