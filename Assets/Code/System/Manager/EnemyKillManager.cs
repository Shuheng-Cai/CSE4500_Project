using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyKillManager : MonoBehaviour {
    public static EnemyKillManager instance;

    Dictionary<string, int> kills = new Dictionary<string, int>();

    public IReadOnlyDictionary<string, int> Kills => kills;

    public int TotalKills {
        get {
            int sum = 0;
            foreach (var v in kills.Values) sum += v;
            return sum;
        }
    }

    void Awake() {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void OnEnable() {
        GameEvent.OnEnemyKilled += RecordKill;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable() {
        GameEvent.OnEnemyKilled -= RecordKill;
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void RecordKill(string enemyType) {
        if (!kills.ContainsKey(enemyType)) kills[enemyType] = 0;
        kills[enemyType]++;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        if (scene.name == "Battle" || scene.name == "BossFight") {
            kills.Clear();
        }
    }
}
