using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalCoin : Coin
{
    // Method
    protected override void PickUp()
    {
        GameEvent.OnCoinCollected?.Invoke(coinData.value);
        Debug.Log(1);         
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
