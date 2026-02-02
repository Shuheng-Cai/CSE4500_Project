using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedCoin : Coin
{
    protected override void PickUp()
    {
        GameEvent.OnCoinCollected?.Invoke(coinData.value);       
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            PickUp();
        }
    }
}
