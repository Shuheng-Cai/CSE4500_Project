using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusCreate : MonoBehaviour
{
    [Header("Spawn Area")]
    public Vector2 areaCenter;
    public Vector2 areaSize = new Vector2(8f, 4f);

    [Header("Coin")]
    public GameObject coinPrefab;
    public int coinCount = 10;
    public float minDistance = 0.6f;

    private List<Vector2> spawnedPositions = new List<Vector2>();

    void Start()
    {
        SpawnCoins();
    }

    public void SpawnCoins()
    {
        spawnedPositions.Clear();

        int attempts = 0;
        int maxAttempts = coinCount * 20;

        while (spawnedPositions.Count < coinCount && attempts < maxAttempts)
        {
            attempts++;

            float randomX = Random.Range(
                areaCenter.x - areaSize.x / 2,
                areaCenter.x + areaSize.x / 2
            );

            float randomY = Random.Range(
                areaCenter.y - areaSize.y / 2,
                areaCenter.y + areaSize.y / 2
            );

            Vector2 randomPos = new Vector2(randomX, randomY);

            bool tooClose = false;
            foreach (Vector2 pos in spawnedPositions)
            {
                if (Vector2.Distance(pos, randomPos) < minDistance)
                {
                    tooClose = true;
                    break;
                }
            }

            if (!tooClose)
            {
                spawnedPositions.Add(randomPos);
                Instantiate(coinPrefab, randomPos, Quaternion.identity);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(areaCenter, areaSize);
    }
}
