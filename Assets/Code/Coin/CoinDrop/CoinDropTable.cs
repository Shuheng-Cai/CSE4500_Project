using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu (menuName = "Game/CoinDropTable")]
public class CoinDropTable : ScriptableObject
{
    public List<CoinData> coins;

    public GameObject DropCoin()
    {
        double totalWeight = 0;

        foreach(var c in coins)
        {
            totalWeight += Math.Pow(c.value, -1);
        }

        double randomWeight = UnityEngine.Random.value * totalWeight;

        double current = 0;
        foreach(var coin in coins)
        {
            current += Math.Pow(1 / coin.value, 2);
            if (randomWeight <= current)
            {
                return coin.coin;
            }
        }

        return null;
    }

}
