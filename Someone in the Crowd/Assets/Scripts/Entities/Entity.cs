using SITC.Tools;
using UnityEngine;

namespace SITC
{
    public class Entity : SitcBehaviour
    {
        #region Private members
        private float _conviction = 0f;
        #endregion Private members

        #region API
        public void Move(Vector3 translation)
        {
            translation = translation.normalized;
            translation *= EntityConfiguration.EntitySpeed;
            translation *= Time.deltaTime;

            transform.position += translation;
        }

        public void SetConviction(float conviction)
        {
            _conviction = conviction;
        }

        public void Alert()
        {

        }
        #endregion API
    }
}