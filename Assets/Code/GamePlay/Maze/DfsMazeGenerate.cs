// ==================================================
// Module: DfsMazeGenerate
// Purpose: Generate Maze Dfs logically
// Author: Shuheng
// Date: 2026/1/16
// Dependencies: Use Stack and DFS to realize maze
// ==================================================


using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;

public class DfsMazeGenerate
{
    // State Tracking
    int maxDepth;
    private static readonly (int x, int y)[] dirs =
    {
        (0,1), (1,0), (0, -1), (-1, 0) 
    };

    public MazeData Generate(int w, int h, Random rng)
    {
        // To generate the logical end and start
        int[,] depth = new int[w, h];

        var maze = new MazeData(w, h);
        var visited = new bool[w,h];
        var stack = new Stack<(int x, int y)>();

        stack.Push((0,0));
        visited[0,0] = true;

        int countMaze = 0;

        while(stack.Count > 0)
        {
            var cur = stack.Peek();

            var neighbors = new List<(int x, int y)>();

            foreach(var d in dirs)
            {
                int nx = cur.x + d.x;
                int ny = cur.y + d.y;

                if(nx>=0 && nx<w && ny>=0 && ny<h && !visited[nx, ny])
                {
                    neighbors.Add((nx, ny));
                }
            }

            if(neighbors.Count == 0)
            {
                stack.Pop();
                continue;
            }

            var next = neighbors[rng.Next(neighbors.Count)];

            depth[next.x, next.y] = depth[cur.x, cur.y] + 1;

            if(depth[next.x, next.y] > maxDepth)
            {
                maxDepth = depth[next.x, next.y];
                maze.End = new (next.x, next.y);
            }

            maze.RemoveWall(cur, next);
            countMaze++;
            visited[next.x, next.y] = true;
            stack.Push(next);
        }

        return maze;
    }
}
