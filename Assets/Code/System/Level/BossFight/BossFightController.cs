using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BossFightController : MonoBehaviour {
    public GameObject bossPrefab;
    public Transform bossSpawnPoint;
    public Transform playerSpawnPoint;

    public TMP_Text bossLevelText;
    public TMP_Text countdownText;
    public float countdownTime = 10;
    
    private GameObject currentBoss;
    private Vector3 bossDeathPosition;

    void Start() {
        if (playerSpawnPoint != null && PlayerManager.instance != null) {
            PlayerManager.instance.transform.position = playerSpawnPoint.position;
        }
        
        SpawnBoss();
        
    }

    void SpawnBoss()
    {
        if (bossPrefab == null || bossSpawnPoint == null)
        {
            Debug.LogError("BossFightController: missing prefab or spawn point");
            return;
        }

        Instantiate(bossPrefab, bossSpawnPoint.position, Quaternion.identity);
    }
}
