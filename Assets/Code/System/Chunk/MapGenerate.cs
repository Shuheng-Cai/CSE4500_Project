using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum TileType
{
    Dust,
    Stone,
    Path
}
public class MapGenerate : MonoBehaviour
{
    public Transform generateCenter;

    // Layer Base
    public Tilemap tilemapBase;
    public int chunkSize = 16;
    Dictionary<TileType, TileBase> TypeToTileMap;
    public List<TileBase> tilesBase;
    float[,] map;
    TileBase[] tilesForBase;   // Store Tilebase

    // Layer Docuration 
    public Tilemap tilemapDecoration;
    public List<TileBase> tilesDecoration;
    TileBase[] tilesForDecoration;
    Dictionary<TileType, TileBase> TypeToDecoration;

    // State Tracking
    public float seed;
    public Vector2Int chunk;
    Vector2Int[] init;

    public int viewRadius = 1; // How many chunk around
    private Vector2Int _lastChunk = new Vector2Int(int.MinValue, int.MinValue);
    private HashSet<Vector2Int> _generated = new HashSet<Vector2Int>();

    // Method
    void Start()
    {
        TypeToTileMap = new Dictionary<TileType, TileBase>
        {
            {TileType.Dust, tilesBase[0]},
            {TileType.Stone, tilesBase[1]},
            {TileType.Path, tilesBase[2]}
        };

        TypeToDecoration = new Dictionary<TileType, TileBase>
        {
            {TileType.Dust, tilesDecoration[0]},
            {TileType.Stone, tilesDecoration[1]},
            {TileType.Path, tilesDecoration[2]}
        };

        tilesForBase = new TileBase[chunkSize * chunkSize];
        tilesForDecoration = new TileBase[chunkSize * chunkSize];
    }

    void Update()
    {
        var cur = GetPlayerChunk();

        if(cur != _lastChunk)
        {
            _lastChunk = cur;
            UpdateChunksAround();
        }
    }

    TileType ChooseFromTileType(float f)
    {
        if (f < 0.35f)
            return TileType.Dust;

        else if (f < 0.65f)
            return TileType.Stone;

        else
            return TileType.Path;
    }

    // Get the chunk where the player is.
    Vector2Int GetPlayerChunk()
    {
        Vector3Int cell = tilemapBase.WorldToCell(PlayerManager.instance.player != null ?  PlayerManager.instance.player.transform.position : generateCenter.position);
        int cx = Mathf.FloorToInt(cell.x / (float)chunkSize);
        int cy = Mathf.FloorToInt(cell.y / (float)chunkSize);
        return new Vector2Int(cx, cy);
    }

    void GenerateChunk(Vector2Int coord)
    {
        int originX = coord.x * chunkSize;
        int originY = coord.y * chunkSize;

        var bounds = new BoundsInt(new Vector3Int(originX, originY, 0),
                                   new Vector3Int(chunkSize, chunkSize, 1));

        map = Noise.GenerateNoiseMap(chunkSize, chunkSize, new Vector2(originX, originY), seed, 0.1f);
        

        for (int y = 0; y < chunkSize; y++)
        {
            for (int x = 0; x < chunkSize; x++)
            {
                tilesForBase[x + y * chunkSize] = TypeToTileMap[ChooseFromTileType(map[x,y])];

                // For Docuration
                int r = Random.Range(0, x + y);
                Debug.Log(r);
                if(r % 12 == 0)
                {
                    tilesForDecoration[x + y * chunkSize] = TypeToDecoration[ChooseFromTileType(map[x,y])];
                }
            }
        }

        tilemapBase.SetTilesBlock(bounds, tilesForBase);
        tilemapDecoration.SetTilesBlock(bounds, tilesForDecoration);
    }

    void UpdateChunksAround()
    {
        for(int dx = -viewRadius; dx <= viewRadius; dx++)
        {
            for(int dy = -viewRadius; dy <= viewRadius; dy++)
            {
                var c = new Vector2Int(GetPlayerChunk().x + dx, GetPlayerChunk().y + dy);
                if(_generated.Add(c))
                    GenerateChunk(c);
            }
        }

        UnloadFarChunks(tilemapBase, GetPlayerChunk());
        UnloadFarChunks(tilemapDecoration, GetPlayerChunk());
    }

    void ClearChunk(Tilemap tilemap, Vector2Int coord)
    {
        int originX = coord.x * chunkSize;
        int originY = coord.y * chunkSize;

        var bounds = new BoundsInt(new Vector3Int(originX, originY, 0),
                                new Vector3Int(chunkSize, chunkSize, 1));

        TileBase[] empty = new TileBase[chunkSize * chunkSize];
        tilemap.SetTilesBlock(bounds, empty);
    }

    int unloadExtra = 1;

    // Clear all the far chunk
    void UnloadFarChunks(Tilemap tilemap, Vector2Int center)
    {
        int keep = viewRadius + unloadExtra;
        var toRemove = new List<Vector2Int>();

        foreach (var c in _generated)
        {
            int dx = Mathf.Abs(c.x - center.x);
            int dy = Mathf.Abs(c.y - center.y);
            if (dx > keep || dy > keep)
                toRemove.Add(c);
        }

        foreach (var c in toRemove)
        {
            ClearChunk(tilemap, c);
            _generated.Remove(c);
        }
    }

}
