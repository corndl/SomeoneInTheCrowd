using SITC.Tools;
using UnityEngine;

namespace SITC.Controls
{
    [RequireComponent(typeof(Entity))]
    public class InputHandler : SitcBehaviour
    {
        #region Private members
        private Entity _entity = null;
        #endregion Private members

        #region Getters
        private Entity Entity { get { _entity = _entity ?? GetComponent<Entity>(); return _entity; } }
        #endregion Getters

        #region Lifecycle
        protected override void DoUpdate()
        {
            base.DoUpdate();

            Vector3 translation = Vector3.zero;

            if (Input.GetKey(KeyCode.RightArrow))
            {
                translation = Vector3.right;
            }

            if (translation != Vector3.zero)
            {
                Entity.Move(translation);
            }
        }
        #endregion Lifecycle
    }
}