using SITC.Tools;
using UnityEngine;

namespace SITC
{
    public class EntityConfiguration : Singleton<EntityConfiguration>
    {
        #region Members
        [Header("Controls"), SerializeField]
        private float _entitySpeed = 1f;

        [Header("Conviction"), SerializeField]
        private AnimationCurve _conviction = null;
        [SerializeField]
        private float _convictionNormalizatioFacton = 0.01f;
        #endregion Members

        #region Public getters
        public static float EntitySpeed { get { return (Instance != null) ? Instance._entitySpeed : 1f; } }
        public static AnimationCurve ConvictionCurve { get { return (Instance != null) ? Instance._conviction : new AnimationCurve(); } }
        public static float ConvictionNormalizationFactor { get { return (Instance != null) ? Instance._convictionNormalizatioFacton : 0.01f; } }
        #endregion Public getters
    }
}