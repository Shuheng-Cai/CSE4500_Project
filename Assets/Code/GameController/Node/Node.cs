using System.Collections.Generic;
using UnityEngine;

public class Node
{
    // Configuration
    // gScore to the start. hScore to the destination.
    public float gScore;
    public float hScore;
    public float penalty;

    // State Tracking
    public Node cameFrom;
    public List<Node> connections;
    public Vector3Int cell;
    public Vector3 position;

    public float FScore()
    {
        return gScore + hScore;
    }
}
