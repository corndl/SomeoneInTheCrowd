#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace SITC.AI
{
    [CustomEditor(typeof(AiPatrolPoints))]
    public class AiPatrolPointsEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Find patrol points"))
            {
                AiPatrolPoints pp = target as AiPatrolPoints;
                pp.InitPositions();
            }
        }
    }
}