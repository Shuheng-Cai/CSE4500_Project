using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    // Configuration
    // gScore to the start. hScore to the destination.
    public float gScore;
    public float hScore;
    public float penalty;

    // State Tracking
    public Node cameFrom;
    public List<Node> connections;

    void Awake()
    {
        NodeManager.instance.allNodes.Add(this);
    }

    public void Start()
    {
        foreach(var node in NodeManager.instance.allNodes)
        {
            if(Vector2.Distance(node.transform.position, transform.position) < 1.5f && node != this)
            {
                connections.Add(node);
            }
        }
    }

    public float FScore()
    {
        return gScore + hScore;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        if(connections.Count > 0)
        {
            foreach(var i in connections)
            {
                Gizmos.DrawLine(transform.position, i.transform.position);
            }
        }
    }
}
