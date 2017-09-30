using SITC.Tools;
using UnityEngine;

namespace SITC.Controls
{
    [RequireComponent(typeof(Entity))]
    public class InputHandler : SitcBehaviour
    {
        #region Data structures
        public enum EDirection
        {
            Right,
            Left,
            Up,
            Down
        }
        #endregion Data structures

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

            if (GetDirection(EDirection.Right))
            {
                translation += Vector3.right;
            }
            if (GetDirection(EDirection.Left))
            {
                translation += Vector3.left;
            }
            if (GetDirection(EDirection.Down))
            {
                translation += Vector3.down;
            }
            if (GetDirection(EDirection.Up))
            {
                translation += Vector3.up;
            }

            return translation;
        }

        private bool GetDirection(EDirection direction)
        {
            switch (direction)
            {
                case EDirection.Right:
                    return Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);

                case EDirection.Down:
                    return Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);

                case EDirection.Left:
                    return Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.Q);

                case EDirection.Up:
                    return Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Z);
            }
            
            return false;
        }
        #endregion Movement
    }
}