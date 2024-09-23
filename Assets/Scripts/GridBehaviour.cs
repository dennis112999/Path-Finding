using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathFinder
{
    /// <summary>
    /// GridBehaviour - 
    /// Responsible for generating the grid,
    /// initializing the player, finding the path, 
    /// and controlling player movement.
    /// </summary>
    public class GridBehaviour : MonoBehaviour
    {
        [Header("Grid")]
        public int Rows = 10;
        public int Columns = 10;
        public int Scale = 1;


        public GameObject GridPrefab;
        private GridState[,] _gridArray;

        public Vector3 leftBottomLocation = new Vector3(0, 0, 0);
        public Vector2 StartPos = new Vector2(0, 0);
        public Vector2 EndPos = new Vector2(2, 2);

        private List<GridState> _path = new List<GridState>();

        [Header("Player")]
        public GameObject PlayerPrefab;
        private GameObject _player;
        public float MoveSpeed = 2f;

        private Tools.PathFinder pathFinder;

        /// <summary>
        /// Initializes the grid and player position
        /// </summary>

        public void Initialize()
        {
            if (!Application.isPlaying) return;
            DestroyAllChildren();

            _gridArray = new GridState[Rows, Columns];
            GenerateGrid();

            _player = Instantiate(PlayerPrefab, new Vector3(StartPos.x, 0, StartPos.y), Quaternion.identity);
            _player.transform.SetParent(transform, false);

            pathFinder = new Tools.PathFinder(_gridArray, Rows, Columns);
        }

        /// <summary>
        /// Starts finding the path and moves the player along the path.
        /// </summary>
        public void ExecutePathfinding()
        {
            if (!Application.isPlaying) return;
            if (pathFinder == null) return;

            _path = pathFinder.FindPath(StartPos, EndPos);
            StartCoroutine(MovePlayerAlongPath());
        }

        /// <summary>
        /// Generates grid
        /// </summary>
        private void GenerateGrid()
        {
            for (int i = 0; i < Columns; i++)
            {
                for (int j = 0; j < Rows; j++)
                {
                    GameObject obj = Instantiate(GridPrefab, new Vector3(leftBottomLocation.x + Scale * i, leftBottomLocation.y, leftBottomLocation.z + Scale * j), Quaternion.identity);
                    obj.transform.SetParent(transform);
                    var gridStat = obj.GetComponent<GridState>();
                    gridStat.x = i;
                    gridStat.y = j;
                    _gridArray[i, j] = gridStat;
                }
            }
        }

        /// <summary>
        /// Destroys all child objects (including the grid and player).
        /// </summary>
        private void DestroyAllChildren()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }

            _path.Clear();
        }

        /// <summary>
        /// Moves the player along the calculated path.
        /// </summary>
        /// <returns></returns>
        private IEnumerator MovePlayerAlongPath()
        {
            foreach (GridState waypoint in _path)
            {
                Vector3 targetPosition = waypoint.transform.position;
                while (Vector3.Distance(_player.transform.position, targetPosition) > 0.1f)
                {
                    _player.transform.position = Vector3.MoveTowards(_player.transform.position, targetPosition, MoveSpeed * Time.deltaTime);
                    yield return null;
                }

            }
        }
    }

}