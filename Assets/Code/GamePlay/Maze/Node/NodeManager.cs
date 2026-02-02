using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NodeManager
{
    public List<Node> allNodes = new List<Node>();
    public Dictionary<Vector3Int, Node> nodeMap = new Dictionary<Vector3Int, Node>();

    private static readonly Vector3Int[] dirs =
    {
        Vector3Int.up, Vector3Int.right, Vector3Int.down, Vector3Int.left
    };

    public void BuildNodeForTile(Tilemap tilemap, TileBase pathTile, TileBase endTile, int tw, int th)
    {
        allNodes.Clear();
        nodeMap.Clear();

        bool IsWalkable(Vector3Int p)
        {
            var t = tilemap.GetTile(p);
            return t == pathTile || t == endTile;
        }

        // Build Node
        for (int x = 0; x < tw; x++)
        for (int y = 0; y < th; y++)
        {
            var cell = new Vector3Int(x, y, 0);
            if (!IsWalkable(cell)) continue;

            var node = new Node();
            node.cell = cell;
            node.position = tilemap.GetCellCenterWorld(cell);
            node.ID = allNodes.Count;
            node.connections = new List<Node>(4);

            nodeMap[cell] = node;
            allNodes.Add(node);
        }

        // Connection
        foreach (var kv in nodeMap)
        {
            var cell = kv.Key;
            var node = kv.Value;

            foreach (var d in dirs)
            {
                var nb = cell + d;
                if (nodeMap.TryGetValue(nb, out var nbNode))
                    node.connections.Add(nbNode);
            }
        }
    }
}
