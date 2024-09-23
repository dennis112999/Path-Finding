using UnityEngine;

namespace PathFinder
{
    /// <summary>
    /// Represents the state of a single grid cell in the pathfinding system.
    /// </summary>
    public class GridState : MonoBehaviour
    {
        /// <summary>
        /// Indicates whether this grid cell has been visited during pathfinding.
        /// </summary>
        public bool visited = false;

        /// <summary>
        /// The x-coordinate of this grid cell in the grid.
        /// </summary>
        public int x = 0;

        /// <summary>
        /// The y-coordinate of this grid cell in the grid.
        /// </summary>
        public int y = 0;
    }
}
