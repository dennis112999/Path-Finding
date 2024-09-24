using System.Collections.Generic;
using UnityEngine;
using PathFinder;

namespace Tools
{
    /// <summary>
    /// A* pathfinding algorithm to find the shortest path between two points on a grid.
    /// </summary>
    public class PathFinderAStar
    {
        private GridState[,] gridArray;
        private int rows, columns;
        private HashSet<Vector2> openSet = new HashSet<Vector2>();

        // Direction vectors for exploring neighbors: right, up, left, down.
        private readonly int[] dx = { 0, 1, 0, -1 };
        private readonly int[] dy = { 1, 0, -1, 0 };

        /// <summary>
        /// Constructor for the pathfinder.
        /// </summary>
        /// <param name="gridArray">The grid states array representing the nodes of the graph.</param>
        /// <param name="rows">Number of rows</param>
        /// <param name="columns">Number of columns</param>
        public PathFinderAStar(GridState[,] gridArray, int rows, int columns)
        {
            this.gridArray = gridArray;
            this.rows = rows;
            this.columns = columns;
        }

        /// <summary>
        /// Checks if a position is in the open list.
        /// </summary>
        /// <param name="position">The position to check.</param>
        /// <returns>True if the position is in the open list, otherwise false.</returns>
        private bool ContainsInOpenList(Vector2 position)
        {
            return openSet.Contains(position);
        }

        /// <summary>
        /// Attempts to find a path from start to end using the A* algorithm.
        /// </summary>
        /// <param name="start">The start position.</param>
        /// <param name="end">The end position.</param>
        /// <returns>A list of GridState objects representing the path, or an empty list if no path is found.</returns>
        public List<GridState> FindPath(Vector2 start, Vector2 end)
        {
            var openList = new PriorityQueue<Node>();
            var cameFrom = new Dictionary<Vector2, Vector2>();
            var gScore = new Dictionary<Vector2, float>();
            var fScore = new Dictionary<Vector2, float>();

            Vector2 startNode = new Vector2((int)start.x, (int)start.y);
            Vector2 endNode = new Vector2((int)end.x, (int)end.y);

            openList.Enqueue(new Node(startNode, 0));
            openSet.Add(startNode);
            gScore[startNode] = 0;
            fScore[startNode] = Heuristic(startNode, endNode);

            while (!openList.IsEmpty())
            {
                var currentNode = openList.Dequeue();
                Vector2 current = currentNode.Position;
                openSet.Remove(current);

                if (current.Equals(endNode))
                {
                    return ReconstructPath(cameFrom, current);
                }

                for (int i = 0; i < 4; i++)
                {
                    Vector2 neighbour = new Vector2(current.x + dx[i], current.y + dy[i]);
                    if (IsInBounds((int)neighbour.x, (int)neighbour.y) && gridArray[(int)neighbour.x, (int)neighbour.y] != null)
                    {
                        float tentativeGScore = gScore[current] + Vector2.Distance(current, neighbour);
                        if (!gScore.ContainsKey(neighbour) || tentativeGScore < gScore[neighbour])
                        {
                            cameFrom[neighbour] = current;
                            gScore[neighbour] = tentativeGScore;
                            float totalCost = gScore[neighbour] + Heuristic(neighbour, endNode);
                            if (!ContainsInOpenList(neighbour))
                            {
                                openList.Enqueue(new Node(neighbour, totalCost));
                                openSet.Add(neighbour);
                            }
                        }
                    }
                }
            }

            return new List<GridState>();
        }

        /// <summary>
        /// Reconstructs the path from the end node to the start node using the cameFrom map.
        /// </summary>
        /// <param name="cameFrom">Map of each node to its predecessor on the path.</param>
        /// <param name="current">The current node to backtrack from.</param>
        /// <returns>A list of GridStates representing the path.</returns>
        private List<GridState> ReconstructPath(Dictionary<Vector2, Vector2> cameFrom, Vector2 current)
        {
            List<GridState> totalPath = new List<GridState>();
            while (cameFrom.ContainsKey(current))
            {
                if (gridArray[(int)current.x, (int)current.y] != null)
                {
                    totalPath.Insert(0, gridArray[(int)current.x, (int)current.y]);
                    current = cameFrom[current];
                }
                else
                {
                    // Handle the case where grid state is null
                    break;
                }
            }
            return totalPath;
        }

        private bool IsInBounds(int x, int y)
        {
            return x >= 0 && x < columns && y >= 0 && y < rows;
        }

        /// <summary>
        /// Calculates the heuristic (Manhattan distance) between two points.
        /// </summary>
        /// <param name="a">The first point.</param>
        /// <param name="b">The second point.</param>
        /// <returns>The Manhattan distance between the two points.</returns>
        private float Heuristic(Vector2 a, Vector2 b)
        {
            // Manhattan distance on a grid
            return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
        }
    }
}
