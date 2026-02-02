using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvent
{
    public static Action<float> OnCoinCollected;

    // Where the enemy die.
    public static Action<Vector3> OnDieEnemy;
}
