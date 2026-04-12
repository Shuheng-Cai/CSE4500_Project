using System.Collections;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

/*
    This is a basic Manager. It must be put in the inspecter in test scene.
    Control Game Scene Change
    Game flow
*/

public enum GameState
{
    MainMenu,
    Battle,
    Maze,
    Paused,
    GameOver,
    Character,
    Store,
    Campsite,
    Bonus,
    BossFight
}

public class GameManager : MonoBehaviour
{
    // Outlet
    public static int runGameSeed { get; private set; }
    public static GameManager instance {get; private set;}
    public static float totalGold {get; private set;}

    // Configuration
    public float everyLevelTime;
    public int Initial_Level = 1;
    public float bonusSceneRate = 1f;
    
    // State Tracking
    public GameState currentState {get; private set;}
    public float battleTimeCounter;
    private bool canCountBattleTime;
    public int currentLevel;
    public bool isTransitioning;

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
            currentState = GameState.Battle;
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
        GameEvent.EnterNoneBattleScene.Invoke();
    }

    public void EnterCampsite()
    {
        currentState = GameState.Campsite;
        SceneManager.LoadScene("Campsite");
        GameEvent.EnterNoneBattleScene.Invoke();
    }

    public void EnterBonusScene() {
        canCountBattleTime = false;
        currentState = GameState.Bonus;
        SceneManager.LoadScene("Bonus");
        GameEvent.EnterNoneBattleScene.Invoke();
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
        
        isTransitioning = false;

        if (scene.name == "Battle")
        {
            PlayerManager.instance.ResetPlayerInBattle();
            canCountBattleTime = true;
        }
        
        if (scene.name == "BossFight")
        {
            PlayerManager.instance.ResetPlayerInBattle();
        }

        // Shrink HP Bar in the store scene to hide it behind the dialog box
        GameObject hpBar = GameObject.Find("Canvas 1/HPBar");
        if (hpBar != null) {
            RectTransform hpRect = hpBar.GetComponent<RectTransform>();                                                                     
            if (scene.name == "Store"){
                hpRect.sizeDelta = new Vector2(250, 30);
                hpRect.anchoredPosition = new Vector2(0, 5);
            } else {
                hpRect.sizeDelta = new Vector2(500, 60);
                hpRect.anchoredPosition = new Vector2(0, 10);
            }
        }
    }

    // Enter next level: first time: generate player | reset player position
    public void EnterNextLevel()
    {
        
        if (isTransitioning) return;
        isTransitioning = true;
        
        if(PlayerManager.instance.player == null) {
            PlayerManager.instance.PlayerGenerate();
            currentLevel = Initial_Level;
        }else {
            currentLevel += 1;
        }
        
        if (IsBossLevel(currentLevel)) {
            canCountBattleTime = false;
            currentState = GameState.BossFight;
            SceneManager.LoadScene("BossFight");
        }else {
            SceneManager.LoadScene("Battle");
                    canCountBattleTime = true;
        }
        
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

            OnLevelFinished(); //Replacing the workflow to OnLevel Finished
            // EnterStore();
        }
    }
    
    //Call when a level is finished
    private void OnLevelFinished() {
        battleTimeCounter = 0f;
        canCountBattleTime = false;
        
        float roll = UnityEngine.Random.Range(0f, 1f);

        if (roll < bonusSceneRate) {
            EnterBonusScene();
        }
        else {
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
    
    public int bossLevelInterval = 5;

    public bool IsBossLevel(int level)
    {
        return level > 0 && level % bossLevelInterval == 0;
    }
    
}
