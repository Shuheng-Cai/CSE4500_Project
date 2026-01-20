using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy")]
public class EnemyData : ScriptableObject
{
    public int enemyID;
    public int speed;
    public float maxLife;
    public float damage;
    public GameObject enemyPrefab;
    public int baseWeight; // Control the weight to spawn;
    public int updateWave; // Control when to spawn;
}
