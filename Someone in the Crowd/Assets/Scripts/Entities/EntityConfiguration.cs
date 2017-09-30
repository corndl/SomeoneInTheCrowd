using SITC.Tools;
using UnityEngine;

namespace SITC
{
    public class EntityConfiguration : Singleton<EntityConfiguration>
    {
        #region Members
        [Header("Controls"), SerializeField]
        private float _entitySpeed = 1f;

        [Header("Conviction"), SerializeField, UnityEngine.Serialization.FormerlySerializedAs("_conviction")]
        private AnimationCurve _convictionInitialiRepartition = null;
        [SerializeField]
        private AnimationCurve _convictionNormalizatioFactor = null;
        [SerializeField]
        private AnimationCurve _influenceRadius = null;
        [SerializeField]
        private AnimationCurve _influenceFactor = null;
        #endregion Members

        #region Public getters
        public static float EntitySpeed { get { return (Instance != null) ? Instance._entitySpeed : 1f; } }
        public static AnimationCurve ConvictionInitialRepartition { get { return (Instance != null) ? Instance._convictionInitialiRepartition : new AnimationCurve(); } }
        public static AnimationCurve ConvictionNormalizationFactor { get { return (Instance != null) ? Instance._convictionNormalizatioFactor : new AnimationCurve(); } }
        public static AnimationCurve InfluenceRadius { get { return (Instance != null) ? Instance._influenceRadius : new AnimationCurve(); } }
        public static AnimationCurve InfluenceFactor { get { return (Instance != null) ? Instance._influenceFactor : new AnimationCurve(); } }
        #endregion Public getters
    }
}