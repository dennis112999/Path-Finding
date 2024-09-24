using UnityEngine;
using UnityEditor;

namespace PathFinder
{
    [CustomEditor(typeof(GridBehaviour))]
    public class GridBehaviorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GridBehaviour gridBehavior = (GridBehaviour)target;

            GUILayout.Space(20);

            GUILayout.BeginVertical();

            GUILayout.Label("Algorithm Method");

            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Breadth-first search"))
            {
                gridBehavior.SelectedAlgorithm = GridBehaviour.PathfindingAlgorithm.BFS;

#if UNITY_EDITOR
                Debug.Log("Breadth-First Search selected.");
#endif
            }

            // Button to select A* Algorithm
            if (GUILayout.Button("AStar"))
            {
                gridBehavior.SelectedAlgorithm = GridBehaviour.PathfindingAlgorithm.AStar;

#if UNITY_EDITOR
                Debug.Log("A* Algorithm selected.");
#endif
            }

            GUILayout.EndHorizontal();

            GUILayout.EndVertical();

            GUILayout.Space(10);

            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Initialize"))
            {
                gridBehavior.Initialize();
            }

            if (GUILayout.Button("ExecutePathfinding"))
            {
                gridBehavior.ExecutePathfinding();
            }

            GUILayout.EndHorizontal();
        }
    }
}
