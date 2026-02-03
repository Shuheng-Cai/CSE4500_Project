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
    Character,
    Store
}

public class GameManager : MonoBehaviour
{
    // Outlet
    public static int runGameSeed { get; private set; }
    public static GameManager instance {get; private set;}
    public static float totalGold {get; private set;}

    // Configuration
    public float everyLevelTime;
    
    // State Tracking
    public GameState currentState {get; private set;}
    public float battleTimeCounter;
    private bool canCountTime;

    // Method
    void Awake()
    {
        instance = this;
        GameSeedInit();
        DontDestroyOnLoad(gameObject);
        currentState = GameState.MainMenu;
        EnterMainMenu();
    }

    void FixedUpdate()
    {
        if (canCountTime)
        {
            StartTimer();
        }
        
    }

    // When Enter the main menu
    public void EnterMainMenu()
    {
        currentState = GameState.MainMenu;
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }

    public void EnterStore()
    {
        canCountTime = false;
        currentState = GameState.Store;
        SceneManager.LoadScene("Store");
        PlayerManager.instance.ResetPlayerPosition();
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

    // First Time start game: choose character
    public void FirstStartGame()
    {
        currentState = GameState.Character;
        SceneManager.LoadScene("CharacterChoose");
    }

    // Enter next level: first time: generate player | reset player position
    public void EnterNextLevel()
    {
        if(PlayerManager.instance.player == null)
        {
            PlayerManager.instance.PlayerGenerate();
        }
        SceneManager.LoadScene("Battle");
        PlayerManager.instance.ResetPlayerPosition();
        canCountTime = true;
    }

    public void CharacterChangePage()
    {
        currentState = GameState.Character;
        SceneManager.LoadScene("CharacterChoose");
    }

    void StartTimer()
    {
        battleTimeCounter += Time.fixedDeltaTime;
        if(battleTimeCounter > everyLevelTime)
        {
            battleTimeCounter = 0;
            EnterStore();
        }
    }
}
