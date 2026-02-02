using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using System.IO;
using Unity.VisualScripting;

public class MazeTilemapRenderer : MonoBehaviour
{
    // Method
    // How thick the wall and path is.
    public void Render(MazeData maze, int wallSize, int pathSize, Tilemap tilemap, TileBase wallTile, TileBase pathTile, TileBase endTile)
    {
        int w = maze.Width;
        int h = maze.Height;
        int step = wallSize + pathSize;

        int tw = wallSize * 2 + (w - 1) * step + pathSize;
        int th = wallSize * 2 + (h - 1) * step + pathSize;

        tilemap.ClearAllTiles();

        // all wall
        for (int x = 0; x < tw; x++)
            for (int y = 0; y < th; y++)
                tilemap.SetTile(new Vector3Int(x, y, 0), wallTile);

        // Dig 
        for (int cx = 0; cx < w; cx++)
        {
            for (int cy = 0; cy < h; cy++)
            {
                int x0 = wallSize + cx * step;
                int y0 = wallSize + cy * step;

                // cell
                FillRect(tilemap, pathTile, x0, y0, pathSize, pathSize);

                // Dig up
                if (cy + 1 < h && !maze.HasWall(cx, cy, Wall.Up))
                    FillRect(tilemap, pathTile, x0, y0 + pathSize, pathSize, wallSize);

                // Dig down
                if (cx + 1 < w && !maze.HasWall(cx, cy, Wall.Right))
                    FillRect(tilemap, pathTile, x0 + pathSize, y0, wallSize, pathSize);
            }
        }

        // End Point
        int ex0 = wallSize + maze.End.x * step;
        int ey0 = wallSize + maze.End.y * step;
        FillRect(tilemap, endTile, ex0, ey0, pathSize, pathSize);
    }

    // Fill a rect area with x0, y0 the init position; w, h the width and height
    void FillRect(Tilemap tm, TileBase tile, int x0, int y0, int w, int h)
    {
        for (int x = x0; x < x0 + w; x++)
            for (int y = y0; y < y0 + h; y++)
                tm.SetTile(new Vector3Int(x, y, 0), tile);
    }
}
