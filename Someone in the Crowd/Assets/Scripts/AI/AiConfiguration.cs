using SITC.Tools;
using UnityEngine;

namespace SITC
{
    public class AiConfiguration : Singleton<AiConfiguration>
    {
        #region Members
        [Header("Pathfinding"), SerializeField]
        private float _targetReachedDistance = 0.1f;
        [SerializeField]
        private float _maxDelayBeforeNextTarget = 3f;
        [SerializeField]
        private float _minimumSpeedRatio = .5f;

        [Header("Debug"), SerializeField]
        private Color _aiTargetGizmoColor = Color.red;
        [SerializeField]
        private float _aiTargetGizmoRadius = 1f;
        [SerializeField]
        private Color _aiTargetLinkGizmoColor = Color.magenta;

        [Header("Oppression"), SerializeField]
        private float _searchResistantRange = 5f;
        //[SerializeField]
        //private float _
        #endregion Members

        #region Public getters
        public static float TargetReachedDistance { get { return (Instance != null) ? Instance._targetReachedDistance : 1f; } }
        public static float MaxDelayBeforeNextTarget { get { return (Instance != null) ? Instance._maxDelayBeforeNextTarget : 1f; } }
        public static float MinimumSpeedRatio { get { return (Instance != null) ? Instance._minimumSpeedRatio : 1f; } }
        public static Color AiTargetGizmoColor { get { return (Instance != null) ? Instance._aiTargetGizmoColor : Color.red; } }
        public static float AiTargetGizmoRadius { get { return (Instance != null) ? Instance._aiTargetGizmoRadius : 1f; } }
        public static Color AiTargetLinkGizmoColor { get { return (Instance != null) ? Instance._aiTargetLinkGizmoColor : Color.red; } }

        public static GUIStyle BoldStyle { get { GUIStyle style = new GUIStyle(); style.fontStyle = FontStyle.Bold; return style; } }
        
        public static float SearchResistantRange { get { return (Instance != null) ? Instance._searchResistantRange : 5f; } }
        #endregion Public getters
    }
}