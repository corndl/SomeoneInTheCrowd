using SITC.Tools;
using UnityEngine;

namespace SITC
{
    public class EntityConfiguration : Singleton<EntityConfiguration>
    {
        #region Members
        [Header("Controls"), SerializeField]
        private float _entitySpeed = 1f;
        #endregion Members

        #region Public getters
        public static float EntitySpeed { get { return (Instance != null) ? Instance._entitySpeed : 1f; } }
        #endregion Public getters
    }
}