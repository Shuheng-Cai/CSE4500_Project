using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    GameEvent: Coin Collected / Enemy Die / PlayerUpgrade / Player Enter Scene
*/

public static class GameEvent
{
    // Coin
    public static Action<float> OnCoinCollected;

    // Where the enemy die.
    public static Action<Vector3> OnDieEnemy;

    // Upgrade Event
    public static Action OnPlayerUpgrade;
    public static Action<AttributeUpgradeType> OnPlayerUpgradeUI;

    // Shooting Event
    public static Action ShootEachBattleLevel;

    // Player Event
    public static Action EnterNoneBattleScene;
    public static Action EnterBattle;
  
}
