// Module: EnemySpawn
// Purpose: Control Eneny to spawen considering the wave
// Invariants: 
// Performance: 
// Dependencies: EnemyData
// Known Tricky Cases: unlock the enemy according to the wave of enemy. And spawn enemy according to the weight.
// Especially: there is a enemyPool, inQueue and out Queue


using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    // Outlets
    public Camera cam;
    public List<GameObject> enemies = new List<GameObject>(); 

    // Configuration
    public float spawnTime;

    // State Tracking
    float camHeight;
    float camWidth;
    float spawnRadius;
    float innerR, outerR;

    // Method
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        camHeight = cam.orthographicSize;
        camWidth  = camHeight * cam.aspect;
        spawnRadius = Mathf.Sqrt(camHeight * camHeight + camWidth * camWidth);
        innerR = spawnRadius + 0.2f;
        outerR = spawnRadius + 1f;

        StartCoroutine(SpawnEnemy());
    }

    void Update()
    {

    }

    // Random point
    public Vector3 RandomPointInCircle()
    {
        float a = Random.value * Mathf.PI * 2f;
        float t = Random.value;
        float r = Mathf.Sqrt(Mathf.Lerp(innerR * innerR, outerR * outerR, t));

        return cam.transform.position + new Vector3(Mathf.Cos(a), Mathf.Sin(a)) * r;
    }

    IEnumerator SpawnEnemy()
    {
        int enemyID = Random.Range(0, enemies.Count());
        Instantiate(enemies[enemyID], RandomPointInCircle(), Quaternion.identity);
        yield return new WaitForSeconds(spawnTime);
        StartCoroutine(SpawnEnemy());
    }
}
