﻿using System.Collections.Generic;
using UnityEngine;
using PathFinder;

namespace Tools
{
    /// <summary>
    /// Path Finder - Breadth-first search
    /// </summary>
    public class PathFinder
    {
        private GridState[,] gridArray;
        private int rows, columns;

        // Direction vectors for exploring neighbours (up, right, down, left)
        private readonly int[] dx = { 0, 1, 0, -1 };
        private readonly int[] dy = { 1, 0, -1, 0 };

        /// <summary>
        /// Initializes the PathFinder with a grid and its size.
        /// </summary>
        public PathFinder(GridState[,] gridArray, int rows, int columns)
        {
            this.gridArray = gridArray;
            this.rows = rows;
            this.columns = columns;
        }

        /// <summary>
        /// Main method to find the path between two points using BFS.
        /// </summary>
        /// <param name="start">Starting position</param>
        /// <param name="end">Ending position</param>
        /// <returns>A list of GridStat representing the path</returns>
        public List<GridState> FindPath(Vector2 start, Vector2 end)
        {
            CalculateVisited(start);
            return BuildPath(start, end);
        }

        /// <summary>
        /// Sets up the initial state of the grid by marking all cells as unvisited.
        /// </summary>
        /// <param name="start">Starting point to mark as visited</param>
        private void InitialSetUp(Vector2 start)
        {
            foreach (GridState gridStat in gridArray)
            {
                gridStat.visited = false;
            }
            gridArray[(int)start.x, (int)start.y].visited = true;
        }

        /// <summary>
        /// Explores the grid and marks all reachable nodes as visited from the start.
        /// </summary>
        /// <param name="start">Starting point</param>
        public void CalculateVisited(Vector2 start)
        {
            InitialSetUp(start);

            for (int step = 1; step < rows * columns; step++)
            {
                foreach (GridState gridStat in gridArray)
                {
                    if (gridStat != null && gridStat.visited)
                    {
                        ExploreNeighbours(gridStat.x, gridStat.y);
                    }
                }
            }
        }

        /// <summary>
        /// Builds the path from the start to the end point using the visited nodes.
        /// </summary>
        /// <param name="start">Starting point</param>
        /// <param name="end">Ending point</param>
        /// <returns>A list of GridStat representing the path</returns>
        public List<GridState> BuildPath(Vector2 start, Vector2 end)
        {
            List<GridState> path = new List<GridState>();
            List<GridState> tempList = new List<GridState>();

            if (IsReachable((int)end.x, (int)end.y))
            {
                int x = (int)start.x, y = (int)start.y;
                path.Add(gridArray[x, y]);

                while (!(x == (int)end.x && y == (int)end.y))
                {
                    AddValidDirections(tempList, x, y);
                    GridState next = FindClosest(gridArray[(int)end.x, (int)end.y].transform, tempList);
                    path.Add(next);

                    x = next.x;
                    y = next.y;
                    tempList.Clear();
                }
            }

            return path;
        }

        /// <summary>
        /// Explores the neighbours of a given node and marks them as visited.
        /// </summary>
        private void ExploreNeighbours(int x, int y)
        {
            for (int i = 0; i < 4; i++)
            {
                int newX = x + dx[i];
                int newY = y + dy[i];
                if (IsInBounds(newX, newY) && !gridArray[newX, newY].visited)
                {
                    SetVisited(newX, newY);
                }
            }
        }

        /// <summary>
        /// Marks a node as visited.
        /// </summary>
        private void SetVisited(int x, int y)
        {
            gridArray[x, y].visited = true;
        }

        /// <summary>
        /// Adds valid directions (neighbours) for exploration to the list.
        /// </summary>
        private void AddValidDirections(List<GridState> tempList, int x, int y)
        {
            for (int i = 0; i < 4; i++)
            {
                int newX = x + dx[i];
                int newY = y + dy[i];
                if (IsInBounds(newX, newY) && gridArray[newX, newY].visited)
                {
                    tempList.Add(gridArray[newX, newY]);
                }
            }
        }

        /// <summary>
        /// Checks if the coordinates are within the grid boundaries.
        /// </summary>
        private bool IsInBounds(int x, int y)
        {
            return x >= 0 && x < columns && y >= 0 && y < rows;
        }

        /// <summary>
        /// Finds the closest node to the target location from a list of nodes.
        /// </summary>
        private GridState FindClosest(Transform targetLocation, List<GridState> list)
        {
            float minDistance = float.MaxValue;
            GridState closest = null;

            foreach (GridState gridState in list)
            {
                // Check if the gridState is null (destroyed) before using it
                if (gridState == null || gridState.transform == null)
                {
                    continue;  // Skip this gridState if it has been destroyed
                }

                float distance = Vector3.Distance(targetLocation.position, gridState.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closest = gridState;
                }
            }

            return closest;
        }

        /// <summary>
        /// Checks if the destination is reachable.
        /// </summary>
        private bool IsReachable(int x, int y)
        {
            return gridArray[x, y] != null && gridArray[x, y].visited;
        }
    }

}