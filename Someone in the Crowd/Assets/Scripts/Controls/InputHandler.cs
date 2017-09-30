using SITC.Tools;
using UnityEngine;

namespace SITC.Controls
{
    [RequireComponent(typeof(Entity), typeof(AlertCone))]
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

        #region Members
        [SerializeField]
        private Vector2 _confinementZone = new Vector2(14f, 7f);
        [SerializeField]
        private float _speedRatioWhileInCone = .5f;
        #endregion Members

        #region Private members
        private Entity _entity = null;
        private Camera _camera = null;
        private AlertCone _cone = null;
        #endregion Private members

        #region Getters
        private Entity Entity { get { _entity = _entity ?? GetComponent<Entity>(); return _entity; } }
        private Camera Camera { get { _camera = _camera ?? FindObjectOfType<Camera>(); return _camera; } }
        private AlertCone Cone { get { return _cone = _cone ?? GetComponent<AlertCone>(); } }
        #endregion Getters

        #region Lifecycle
        protected override void DoUpdate()
        {
            base.DoUpdate();

            GetCone();
            Vector3 translation = GetDirection();

            if (translation != Vector3.zero)
            {
                translation = translation.normalized;

                if (Cone.IsActive())
                {
                    translation *= _speedRatioWhileInCone;
                }

                Entity.Move(translation);
            }
        }

        private void OnDrawGizmos()
        {
            Vector3 topLeft = new Vector3(-_confinementZone.x / 2, _confinementZone.y / 2);
            Vector3 bottomLeft = new Vector3(-_confinementZone.x / 2, -_confinementZone.y / 2);
            Vector3 topRight = new Vector3(_confinementZone.x / 2, _confinementZone.y / 2);
            Vector3 bottomRight = new Vector3(_confinementZone.x / 2, -_confinementZone.y / 2);

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(topLeft, topRight);
            Gizmos.DrawLine(topRight, bottomRight);
            Gizmos.DrawLine(bottomRight, bottomLeft);
            Gizmos.DrawLine(bottomLeft, topLeft);
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

            translation = Confine(translation);
            return translation;
        }

        private bool GetDirection(EDirection direction)
        {
            switch (direction)
            {
                case EDirection.Right:
                    return (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && transform.position.x < _confinementZone.x / 2;

                case EDirection.Down:
                    return (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && transform.position.y > - _confinementZone.y / 2;

                case EDirection.Left:
                    return (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.Q)) && transform.position.x > - _confinementZone.x / 2;

                case EDirection.Up:
                    return (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Z)) && transform.position.y < _confinementZone.y / 2;
            }
            
            return false;
        }

        private Vector3 Confine(Vector3 translation)
        {
            if (translation.x > _confinementZone.x / 2)
            {
                translation.x = _confinementZone.x / 2;
            }
            else if (translation.x < -_confinementZone.x / 2)
            {
                translation.x = -_confinementZone.x / 2;
            }

            if (translation.y > _confinementZone.y / 2)
            {
                translation.y = _confinementZone.y / 2;
            }
            else if (translation.y < -_confinementZone.y / 2)
            {
                translation.y = -_confinementZone.y / 2;
            }

            return translation;
        }
        #endregion Movement

        #region Cone
        private void GetCone()
        {
            if (MustCancelCone())
            {
                Cone.StopCone(true);
            }
            else if (CanGrowCone())
            {
                Cone.GrowCone();

                Vector3 mousePosition = Camera.ScreenToWorldPoint(Input.mousePosition).SetZ(0f);
                Cone.DrawCone(mousePosition);
            }
            else if (MustStopCone())
            {
                Cone.StopCone(false);
            }
        }

        private bool CanGrowCone()
        {
            return Input.GetMouseButton(0);
        }

        private bool MustStopCone()
        {
            return Input.GetMouseButtonUp(0);
        }

        private bool MustCancelCone()
        {
            return Input.GetMouseButtonDown(1);
        }
        #endregion Cone
    }
}