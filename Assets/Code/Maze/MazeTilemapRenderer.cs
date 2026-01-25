using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public class MazeTilemapRenderer : MonoBehaviour
{
    // Outlet
    public static MazeTilemapRenderer instance;
    public static MazeData mazeData { get; private set; }
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private TileBase wallTile;
    [SerializeField] private TileBase pathTile;

    // Configuration
    private static int seed = (int)GameContext_Seed.MazeSeed;
    private System.Random rng = new System.Random(seed);
    [SerializeField] private Vector3Int origin = Vector3Int.zero;


    // Method
    void Awake()
    {
        instance = this;
        var generate = new DfsMazeGenerate();
        mazeData = generate.Generate(20, 20, rng);
        Render(mazeData);
    }

    public void Render(MazeData maze)
    {
        int w = maze.Width;
        int h = maze.Height;

        int tw = 2 * w + 1;
        int th = 2 * h + 1;

        tilemap.ClearAllTiles();

        // 1) File the wall
        for (int x = 0; x < tw; x++)
        {
            for (int y = 0; y < th; y++)
            {
                tilemap.SetTile(origin + new Vector3Int(x, y, 0), wallTile);
            }
        }

        // 2) Generate Maze
        for (int cx = 0; cx < w; cx++)
        {
            for (int cy = 0; cy < h; cy++)
            {
                // cell center
                SetPath(2 * cx + 1, 2 * cy + 1);

                // Dig Up wall
                if (!maze.HasWall(cx, cy, Wall.Up) && cy + 1 < h)
                    SetPath(2 * cx + 1, 2 * cy + 2);

                // Dig Right wall
                if (!maze.HasWall(cx, cy, Wall.Right) && cx + 1 < w)
                    SetPath(2 * cx + 2, 2 * cy + 1);
            }
        }

        void SetPath(int x, int y)
        {
            tilemap.SetTile(origin + new Vector3Int(x, y, 0), pathTile);
        }
    }
}
