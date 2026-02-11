using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
    // Outlet
    public static GoldManager instance;
    public float currentGold {get; private set;}
    public CoinDropTable coinDropTable;

    // Method
    void OnEnable()
    {
        GameEvent.OnCoinCollected += AddCoin;
        GameEvent.OnDieEnemy += GenerateCoin;
    }

    void OnDisable()
    {
        GameEvent.OnCoinCollected -= AddCoin;
        GameEvent.OnDieEnemy -= GenerateCoin;
    }

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddCoin(float value)
    {
        currentGold += value;
    }

    // If player have enough coin, we can cost. TODO: if coin no enough, invoke a screen shake? or a conversation
    public bool CostCoin(float value)
    {
        if(currentGold > value)
        {
            currentGold -= value;
            return true;
        }

        return false;
    }
    
    public void GenerateCoin(Vector3 coinPosition)
    {
        Instantiate(coinDropTable.DropCoin(), coinPosition, Quaternion.identity);
    }
}
