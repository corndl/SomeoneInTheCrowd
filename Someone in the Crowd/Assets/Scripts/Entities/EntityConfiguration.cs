using SITC.Tools;
using System;
using System.Collections.Generic;
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
        [SerializeField]
        private AnimationCurve _alertConvictionImpact = null;

        [Header("Appearance randomization"), SerializeField]
        private List<Sprite> _hairSprites = null;
        [SerializeField]
        private List<Sprite> _headSprites = null;
        [SerializeField]
        private List<Sprite> _bodySprites = null;

        [Header("Conviction sprites"), SerializeField]
        private float _minimumConvictionForOppressor = -.5f;
        [SerializeField]
        private Sprite _oppressorSprite = null;
        [SerializeField]
        private Sprite _neutralSprite = null;
        [SerializeField]
        private float _minimumConvictionForResistant = .5f;
        [SerializeField]
        private Sprite _resistantSprite = null;
        #endregion Members

        #region Public getters
        public static float EntitySpeed { get { return (Instance != null) ? Instance._entitySpeed : 1f; } }

        public static AnimationCurve ConvictionInitialRepartition { get { return (Instance != null) ? Instance._convictionInitialiRepartition : new AnimationCurve(); } }
        public static AnimationCurve ConvictionNormalizationFactor { get { return (Instance != null) ? Instance._convictionNormalizatioFactor : new AnimationCurve(); } }
        public static AnimationCurve InfluenceRadius { get { return (Instance != null) ? Instance._influenceRadius : new AnimationCurve(); } }
        public static AnimationCurve InfluenceFactor { get { return (Instance != null) ? Instance._influenceFactor : new AnimationCurve(); } }
        public static AnimationCurve AlertConvictionImpact { get { return (Instance != null) ? Instance._alertConvictionImpact : new AnimationCurve(); } }

        public static List<Sprite> HairSprites { get { return (Instance != null) ? Instance._hairSprites : null; } }
        public static List<Sprite> HeadSprites { get { return (Instance != null) ? Instance._headSprites : null; } }
        public static List<Sprite> BodySprites { get { return (Instance != null) ? Instance._bodySprites : null; } }

        public static float MinimumConvictionForOppressor { get { return (Instance != null) ? Instance._minimumConvictionForOppressor : -.5f; } }
        public static Sprite OppressorSprite { get { return (Instance != null) ? Instance._oppressorSprite : null; } }
        public static Sprite NeutralSprite { get { return (Instance != null) ? Instance._neutralSprite : null; } }
        public static float MinimumConvictionForResistant { get { return (Instance != null) ? Instance._minimumConvictionForResistant : .5f; } }
        public static Sprite ResistantSprite { get { return (Instance != null) ? Instance._resistantSprite : null; } }
        #endregion Public getters
    }
}