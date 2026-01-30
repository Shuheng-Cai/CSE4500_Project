using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MazeLevelManager: MonoBehaviour
{
    // Outlet
    public static MazeLevelManager instance;
    // Configuration
    public int level = 1;
    public int character = 5;
    public int difficulty = 6;
    public int runNumber = 7;
    private static int seed;
    private System.Random rng;
    [SerializeField] private Vector3Int origin = Vector3Int.zero;
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private TileBase wallTile;
    [SerializeField] private TileBase pathTile;
    [SerializeField] private TileBase endPointTile;
    public static MazeData mazeData { get; private set; }
    public NodeManager nodeManager;
    public int wallSize = 2;
    public int pathSize = 1;
    public int mazeWidth;
    public int mazeHeight;
    [SerializeField] private MazeTilemapRenderer mazeTilemapRenderer;
    public GameObject enemy;

    void Awake()
    {
        instance = this;
        nodeManager = new NodeManager();

        seed = (int)MazeSeedManager.MazeSeed;
        rng = new System.Random(seed);
        var dfsGenerate = new DfsMazeGenerate();
        mazeData = dfsGenerate.Generate(mazeWidth, mazeHeight, rng);

        int step = wallSize + pathSize;
        int tw = wallSize * 2 + (mazeWidth - 1) * step + pathSize;
        int th = wallSize * 2 + (mazeHeight - 1) * step + pathSize;

        mazeTilemapRenderer.Render(mazeData, wallSize, pathSize, tilemap, wallTile, pathTile, endPointTile);
        nodeManager.BuildNodeForTile(tilemap, pathTile, endPointTile, tw, th);
        MazeSeedManager.MazeInitFromLevel(this);
        Debug.Log($"MazeSeed = {MazeSeedManager.MazeSeed}");
    }

    void Start()
    {
        Vector3Int deadEnd = GetDeadEnds(mazeData)[3];
        GameObject _enemy = Instantiate(enemy, nodeManager.nodeMap[deadEnd].position, Quaternion.identity);
        _enemy.GetComponent<TestAStar>().start = nodeManager.nodeMap[deadEnd];
    }

    List<Vector3Int> GetDeadEnds(MazeData maze)
    {
        var res = new List<Vector3Int>();
        for (int x = 0; x < maze.Width; x++)
        for (int y = 0; y < maze.Height; y++)
        {
            int open = 0;
            if (!maze.HasWall(x,y,Wall.Up)) open++;
            if (!maze.HasWall(x,y,Wall.Right)) open++;
            if (!maze.HasWall(x,y,Wall.Down)) open++;
            if (!maze.HasWall(x,y,Wall.Left)) open++;

            if (open == 1) res.Add(new Vector3Int(wallSize + x * (wallSize + pathSize), wallSize + y * (wallSize + pathSize), 0));
        }
        return res;
    }

    // void OnDrawGizmos()
    // {
    //     if (nodeManager.allNodes == null) return;

    //     Gizmos.color = Color.red;
    //     foreach (var node in nodeManager.allNodes)
    //     {
    //         if (node?.connections == null) continue;
    //         foreach (var c in node.connections)
    //         {
    //             if (c == null) continue;
    //             Gizmos.DrawLine(node.position, c.position);
    //         }
    //     }
    // }
}
