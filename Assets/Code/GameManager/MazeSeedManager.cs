using System;

public static class MazeSeedManager
{
    public static int MazeSeed { get; private set; }
    public static bool MazeInitialized { get; private set; }

    public static void MazeInitFromLevel(MazeLevelManager lc)
    {
        if (MazeInitialized) return;
        if (lc == null) throw new ArgumentNullException(nameof(lc));

        MazeSeed = HashCode.Combine(lc.level, lc.character, lc.difficulty, lc.runNumber);
        MazeInitialized = true;
    }

    public static void ResetSeed()
    {
        MazeInitialized = false;
    }
}
