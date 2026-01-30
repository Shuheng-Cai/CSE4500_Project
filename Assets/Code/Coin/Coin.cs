using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Coin : MonoBehaviour
{
    // Configuration
    public CoinData coinData;

    // Method
    protected abstract void PickUp();
}
