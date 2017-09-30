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

            Vector3 translation = GetDirection();

            if (translation != Vector3.zero)
            {
                Entity.Move(translation);
            }
        }
        #endregion Lifecycle

        #region Movement
        private Vector3 GetDirection()
        {
            Vector3 translation = Vector3.zero;

            if (Input.GetKey(KeyCode.RightArrow))
            {
                translation += Vector3.right;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                translation += Vector3.left;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                translation += Vector3.down;
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                translation += Vector3.up;
            }

            return translation;
        }
        #endregion Movement
    }
}