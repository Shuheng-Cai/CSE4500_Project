using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Mushroom : Enemy {
    
    public GameObject smallMushroomPrefab;
    public int splitCount;
    public float smallMushroomSpawnSeperation;
    
    protected override void Move() {
        Vector3 direction = (target - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }
    
    protected override void OnHitEnemy()
    {   
        Split();
        
        base.OnHitEnemy();
    }

    protected override void Die() {
        
        Split();

        base.Die();
    }

    private void Split()
    {
        for (int i = 0; i < splitCount; i++)
        {
            float t = splitCount == 1 ? 0f : i / (float)(splitCount - 1);
            float offSet = Mathf.Lerp(-smallMushroomSpawnSeperation, smallMushroomSpawnSeperation, t);
            Vector3 spawnPoint = transform.position + new Vector3(offSet, 0f, 0f);
            
            Instantiate(smallMushroomPrefab, spawnPoint, Quaternion.identity);
        }
    }
}
