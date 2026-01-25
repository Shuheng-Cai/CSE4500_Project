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

    // State Tracking
    public List<Node> path = new List<Node>();

    public float moveSpeed;

    public bool cnaWalk = true;

    // Method
    void Start()
    {
        FindShortestPath();
        transform.position = path[0].transform.position;
        rb = transform.GetComponent<Rigidbody2D>();
        StartCoroutine(Movement(0));
    }

    void Update()
    {
        
    }

    private void FindShortestPath()
    {
        path = AStarManager.instance.GeneratePath(start, end);
    }

    IEnumerator Movement(int i)
    {
        // Traversal the Paht
        while (i < path.Count - 1)
        {
            Node nextNode = path[i + 1];
            Vector2 targetPos = nextNode.transform.position;

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
