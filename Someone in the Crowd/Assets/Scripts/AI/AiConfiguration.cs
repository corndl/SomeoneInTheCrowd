using SITC.Tools;
using UnityEngine;

namespace SITC
{
    public class AiConfiguration : Singleton<AiConfiguration>
    {
        #region Members
        [Header("Pathfinding"), SerializeField]
        private float _targetReachedDistance = 0.1f;

        [Header("Debug"), SerializeField]
        private Color _aiTargetGizmoColor = Color.red;
        [SerializeField]
        private float _aiTargetGizmoRadius = 1f;
        [SerializeField]
        private Color _aiTargetLinkGizmoColor = Color.magenta;
        #endregion Members

        #region Public getters
        public static float TargetReachedDistance { get { return (Instance != null) ? Instance._targetReachedDistance : 1f; } }
        public static Color AiTargetGizmoColor { get { return (Instance != null) ? Instance._aiTargetGizmoColor : Color.red; } }
        public static float AiTargetGizmoRadius { get { return (Instance != null) ? Instance._aiTargetGizmoRadius : 1f; } }
        public static Color AiTargetLinkGizmoColor { get { return (Instance != null) ? Instance._aiTargetLinkGizmoColor : Color.red; } }

        public static GUIStyle BoldStyle { get { GUIStyle style = new GUIStyle(); style.fontStyle = FontStyle.Bold; return style; } }
        #endregion Public getters
    }
}