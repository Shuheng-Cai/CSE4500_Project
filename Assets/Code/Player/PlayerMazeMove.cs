using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMazeMove : MonoBehaviour
{

    // Outlet
    private MazeLevelController mazeController;
    private MazeData maze;
    public Tilemap tilemap;

    // Configuration
    private static readonly (int x, int y)[] dirs =
    {
        (0,1), (1,0), (0, -1), (-1, 0) 
    };
    public float speed;

    // State Tracking
    public Vector3Int gridPosition { get; private set;}
    Vector3Int dir = Vector3Int.zero;
    bool isMoving = false;
    public Node currentNode;
    private int currentNodeID;
    private Dictionary<Vector3Int, Node> nodeMap;

    // Method
    void Start()
    {
        mazeController = MazeLevelController.instance;
        maze = MazeLevelController.mazeData;
        transform.position = MazeLevelController.instance.nodeManager.allNodes[0].position;
        currentNode = MazeLevelController.instance.nodeManager.allNodes[0];
        gridPosition = MazeLevelController.instance.nodeManager.allNodes[0].cell;
        nodeMap = MazeLevelController.instance.nodeManager.nodeMap;
    }

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        currentNode = MazeLevelController.instance.nodeManager.allNodes[currentNodeID];
        dir = new Vector3Int(0,0);

        if (Input.GetKey(KeyCode.W)) dir = Vector3Int.up;
        if (Input.GetKey(KeyCode.S)) dir = Vector3Int.down;
        if (Input.GetKey(KeyCode.A)) dir = Vector3Int.left;
        if (Input.GetKey(KeyCode.D)) dir = Vector3Int.right;

        Vector3Int next = currentNode.cell + dir;
        if (dir != new Vector3Int(0,0) && nodeMap.ContainsKey(next) && !isMoving && currentNode.connections.Contains(nodeMap[next]))
        {
            StartCoroutine(MoveToWorldPosition(next));
            currentNodeID = nodeMap[next].ID;
            Debug.Log(currentNodeID);
        }
    }

    IEnumerator MoveToWorldPosition(Vector3Int targetGrid)
    {
        Vector3 start = transform.position;
        Vector3 end = nodeMap[targetGrid].position;

        isMoving = true;

        float t = 0f;
        float duration = 1 / speed;

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
