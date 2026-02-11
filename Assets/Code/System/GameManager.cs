using System.Collections;
using UnityEngine;
using System;
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
    private bool canCountBattleTime;
    public int currentLevel;

    // Method
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

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
        if (canCountBattleTime)
        {
            StartBattleTimer();
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
        canCountBattleTime = false;
        currentState = GameState.Store;
        SceneManager.LoadScene("Store");
        PlayerManager.instance.EnterStore();
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

    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Battle")
        {
            PlayerManager.instance.ResetPlayerInBattle();
            canCountBattleTime = true;
        }
    }

    // Enter next level: first time: generate player | reset player position
    public void EnterNextLevel()
    {
        if(PlayerManager.instance.player == null)
        {
            PlayerManager.instance.PlayerGenerate();
            currentLevel = 0;
        }
        SceneManager.LoadScene("Battle");
        currentLevel += 1;
        canCountBattleTime = true;
    }

    public void CharacterChangePage()
    {
        currentState = GameState.Character;
        SceneManager.LoadScene("CharacterChoose");
    }

    // Start battle -> start timer and win this round
    void StartBattleTimer()
    {
        battleTimeCounter += Time.fixedDeltaTime;
        if(battleTimeCounter > everyLevelTime)
        {
            battleTimeCounter = 0;
            EnterStore();
        }
    }

    // When player die
    public void EnterGameOver()
    {
        StopAllCoroutines();
        CancelInvoke();
        StartCoroutine(GameOverFlow());
    }

    private IEnumerator GameOverFlow()
    {
        SceneManager.LoadScene("GameOver");

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene("BootScene");
        Destroy(gameObject);
    }
}
