// ==================================================
// Module: MazeData
// Purpose: Maze Data 
// Author: Shuheng
// Date: 2026/1/16
// Dependencies: Use binary to present Wall. RemoveWall and Check if has wall.
// ==================================================

using System;
using Unity.Collections;
using UnityEngine;

[Flags]
public enum Wall
{
    None = 0,
    Up = 1 << 0,
    Right = 1 << 1,
    Down = 1 << 2,
    Left = 1 << 3,
    All = Up | Right | Down | Left
}

public class MazeData
{
    public int Width { get; }
    public int Height { get; }
    private Wall[,] walls;

    public MazeData(int w, int h)
    {
        Width = w;
        Height = h;
        walls = new Wall[w, h];

        for (int x = 0; x < w; x++)
            for (int y = 0; y < h; y++)
                walls[x, y] = Wall.All;
    }

    public bool HasWall(int x, int y, Wall w) => (walls[x, y] & w) != 0;

    public void RemoveWall((int x, int y) a, (int x, int y) b)
    {
        int dx = b.x - a.x;
        int dy = b.y - a.y;

        // Right
        if (dx == 1 && dy == 0)
        {
            walls[a.x, a.y] &= ~Wall.Right;
            walls[b.x, b.y] &= ~Wall.Left;
        }
        // Left
        else if (dx == -1 && dy == 0)
        {
            walls[a.x, a.y] &= ~Wall.Left;
            walls[b.x, b.y] &= ~Wall.Right;
        }
        // Up
        else if (dx == 0 && dy == 1)
        {
            walls[a.x, a.y] &= ~Wall.Up;
            walls[b.x, b.y] &= ~Wall.Down;
        }
        // Down
        else if (dx == 0 && dy == -1)
        {
            walls[a.x, a.y] &= ~Wall.Down;
            walls[b.x, b.y] &= ~Wall.Up;
        }
        else
        {
            throw new ArgumentException($"Cells are not adjacent: a={a}, b={b}");
        }
    }
}

