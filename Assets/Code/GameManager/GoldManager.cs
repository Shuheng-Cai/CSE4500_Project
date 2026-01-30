using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
    // Outlet
    public static GoldManager instance;
    public int currentGold;
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

    public void AddCoin(int value)
    {
        currentGold += value;
    }
    
    public void GenerateCoin(Vector3 coinPosition)
    {
        Instantiate(coinDropTable.DropCoin(), coinPosition, Quaternion.identity);
    }
}
