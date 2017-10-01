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
        [SerializeField]
        private float _takeAwaySpeedRatio = .8f;
        [SerializeField]
        private Vector2 _minMaxTakenAwayCooldownBeforeReturn = Vector2.zero;
        [SerializeField]
        private Vector2 _minMaxTookAwayCooldownBeforeOppression = Vector2.zero;
        [SerializeField]
        private float _convictionAfterTakeAway = -.9f;
        [SerializeField]
        private float _minConvictionForTakeAway = .3f;
        [SerializeField]
        private float _maxPursuitDuration = 5f;

        [Header("Witness"), SerializeField]
        private AnimationCurve _witnessRange = null;
        [SerializeField]
        private AnimationCurve _witnessDuration = null;

        [Header("Victory"), SerializeField]
        private float _ratioInPercentForVictory = 33f;
        [SerializeField]
        private float _ratioInPercentForDefeat = 50f;
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
        public static float TakeAwaySpeedRatio { get { return (Instance != null) ? Instance._takeAwaySpeedRatio : .8f; } }
        public static Vector2 MinMaxTakenAwayCooldownBeforeReturn { get { return (Instance != null) ? Instance._minMaxTakenAwayCooldownBeforeReturn : Vector2.zero; } }
        public static Vector2 MinMaxTookAwayCooldownBeforeOppression { get { return (Instance != null) ? Instance._minMaxTookAwayCooldownBeforeOppression : Vector2.zero; } }
        
        public static AnimationCurve WitnessRange { get { return (Instance != null) ? Instance._witnessRange : null; } }
        public static AnimationCurve WitnessDuration { get { return (Instance != null) ? Instance._witnessDuration : null; } }

        public static float ConvictionAfterTakeAway { get { return (Instance != null) ? Instance._convictionAfterTakeAway : -1f; } }

        public static float RatioInPercentForVictory { get { return (Instance != null) ? Instance._ratioInPercentForVictory : 33f; } }
        public static float RatioInPercentForDefeat { get { return (Instance != null) ? Instance._ratioInPercentForDefeat : 33f; } }

        public static float MinConvictionForTakeAway { get { return (Instance != null) ? Instance._minConvictionForTakeAway : .3f; } }

        public static float MaxPursuitDuration { get { return (Instance != null) ? Instance._maxPursuitDuration : .3f; } }
        #endregion Public getters
    }
}