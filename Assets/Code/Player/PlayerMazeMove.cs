using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMazeMove : MonoBehaviour
{

    // Outlet
    private MazeTilemapRenderer mazeTilemapRenderer;
    private MazeData maze;
    public Tilemap tilemap;

    // Configuration
    private static readonly (int x, int y)[] dirs =
    {
        (0,1), (1,0), (0, -1), (-1, 0) 
    };

    // State Tracking
    public Vector2Int gridPosition { get; private set;}
    Vector2Int dir = Vector2Int.zero;
    bool isMoving = false;

    // Method
    void Start()
    {
        mazeTilemapRenderer = MazeTilemapRenderer.instance;
        maze = MazeTilemapRenderer.mazeData;
        transform.position = GridToWorld(new Vector2Int(0, 0));
    }

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        dir = new Vector2Int(0,0);

        if (Input.GetKey(KeyCode.W)) dir = Vector2Int.up;
        if (Input.GetKey(KeyCode.S)) dir = Vector2Int.down;
        if (Input.GetKey(KeyCode.A)) dir = Vector2Int.left;
        if (Input.GetKey(KeyCode.D)) dir = Vector2Int.right;

        if (dir != new Vector2Int(0,0) && CanMove(gridPosition, dir) && !isMoving)
        {
            gridPosition += dir;
            StartCoroutine(MoveToWorldPosition(gridPosition));
        }

    }

    bool CanMove(Vector2Int from, Vector2Int dir)
    {
        if (dir == Vector2Int.up)
            return !maze.HasWall(from.x, from.y, Wall.Up);
        if (dir == Vector2Int.down)
            return !maze.HasWall(from.x, from.y, Wall.Down);
        if (dir == Vector2Int.left)
            return !maze.HasWall(from.x, from.y, Wall.Left);
        if (dir == Vector2Int.right)
            return !maze.HasWall(from.x, from.y, Wall.Right);

        return false;
    }

    Vector3 GridToWorld(Vector2Int gp)
    {
        int step = mazeTilemapRenderer.wallSize + mazeTilemapRenderer.pathSize;
        int x0 = mazeTilemapRenderer.wallSize + gp.x * step;
        int y0 = mazeTilemapRenderer.wallSize + gp.y * step;

        // center cell
        int centerOffset = mazeTilemapRenderer.pathSize / 2; // pathSize=1 ->0, =2->1, =3->1
        var cellPos = new Vector3Int(x0 + centerOffset, y0 + centerOffset, 0);

        return tilemap.GetCellCenterWorld(cellPos);
    }

    IEnumerator MoveToWorldPosition(Vector2Int targetGrid)
    {
        Vector3 start = transform.position;
        Vector3 end = GridToWorld(targetGrid);

        isMoving = true;

        float t = 0f;
        float duration = 0.15f;

        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            transform.position = Vector3.Lerp(start, end, t);
            yield return null;
        }

        transform.position = end;
        isMoving = false;
    }
}
