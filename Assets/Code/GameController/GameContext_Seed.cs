// ==================================================
// Module: GameContext
// Purpose: Store all the seed.
// Author: Shuheng
// Date: 2026/1/16
// Dependencies: MazeInit.
// ==================================================

using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class GameContext_Seed
{
    public static long MazeSeed {get; private set; }
    public static bool MazeInitialized {get; private set;}

    // Method
    public static void MazeInit(long seed)
    {
        if (!MazeInitialized)
        {
            MazeSeed = seed;
            MazeInitialized = true;
        }
    }

    public static void GetSeed(string UserInput = null)
    {
        if(!string.IsNullOrEmpty(UserInput))
            MazeSeed = UserInput.GetHashCode();

        MazeSeed = DateTime.UtcNow.Ticks;
    }
}
