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
