using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class TestAStar : MonoBehaviour
{
    // Outlet
    public Node start;
    public Node end;
    Rigidbody2D rb;
    public AStarManager aStarManager;

    // State Tracking
    public List<Node> path = new List<Node>();
    public float moveSpeed;
    public bool cnaWalk = true;


    // Method
    void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>();
        aStarManager = FindObjectOfType<AStarManager>();

        end = GameObject.FindWithTag("Player").GetComponent<PlayerMazeMove>().currentNode;
        FindShortestPath();
        StartCoroutine(Movement(0));
    }

    void Update()
    {

    }

    private void FindShortestPath()
    {
        path = aStarManager.GeneratePath(start, end);
    }

    IEnumerator Movement(int i)
    {
        Debug.Log(1);
        // Traversal the Paht
        while (i < path.Count - 1)
        {
            Node nextNode = path[i + 1];
            Vector2 targetPos = nextNode.position;

            // Move to the Next Node
            while (Vector2.Distance(transform.position, targetPos) > 0.01f)
            {
                Vector2 direction = (targetPos - (Vector2)transform.position).normalized;
                float step = moveSpeed * Time.fixedDeltaTime;
                Vector2 newPos = Vector2.MoveTowards(rb.position, targetPos, step);
                rb.MovePosition(newPos);

                yield return new WaitForFixedUpdate();
            }

            //transform.position = targetPos;
            i++;

            yield return null;
        }
    }
}
