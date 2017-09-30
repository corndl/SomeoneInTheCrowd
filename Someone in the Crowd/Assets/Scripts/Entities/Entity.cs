using SITC.Tools;
using UnityEngine;

namespace SITC
{
    public class Entity : SitcBehaviour
    {
        #region Private members
        private float _conviction = 0f;
        #endregion Private members

        #region Lifecycle
        protected override void DoUpdate()
        {
            base.DoUpdate();

        }
        #endregion Lifecycle

        #region API
        /// <summary>
        /// Does not normalize vector !
        /// </summary>
        public void Move(Vector3 translation)
        {
            translation *= EntityConfiguration.EntitySpeed;
            translation *= Time.deltaTime;

            transform.position += translation;
        }

        public void SetConviction(float conviction)
        {
            _conviction = conviction;
        }

        public void AddConviction(float delta)
        {
            _conviction += delta;
        }

        public float GetConviction()
        {
            return _conviction;
        }

        public void Alert()
        {

        }
        #endregion API
    }
}