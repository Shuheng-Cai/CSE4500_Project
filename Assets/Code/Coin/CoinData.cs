using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/CoinData")]
public class CoinData : ScriptableObject
{
    public int value;
    public GameObject coin;
}
