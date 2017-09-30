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

        #region Members
        [SerializeField]
        private Vector2 _confinementZone = new Vector2(14f, 7f);
        [Header("Cone"), SerializeField]
        private float _coneMinimumSize = 1f;
        [SerializeField]
        private float _coneMaximumSize = 5f;
        [SerializeField]
        private float _coneAngle = 45f;
        [SerializeField]
        private float _coneGrowthFactor = 1f;

        [Header("Cone rendering"), SerializeField]
        private LineRenderer _left = null;
        [SerializeField]
        private LineRenderer _right = null;
        [SerializeField]
        private LineRenderer _joint = null;
        #endregion Members

        #region Private members
        private Entity _entity = null;
        private Camera _camera = null;
        #endregion Private members

        #region Getters
        private Entity Entity { get { _entity = _entity ?? GetComponent<Entity>(); return _entity; } }
        private Camera Camera { get { _camera = _camera ?? FindObjectOfType<Camera>(); return _camera; } }
        #endregion Getters

        #region Lifecycle
        protected override void DoUpdate()
        {
            base.DoUpdate();

            GetCone();
            Vector3 translation = GetDirection();

            if (translation != Vector3.zero)
            {
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

            if (_cone > 0f)
            {
                return translation;
            }

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
        private float _cone = 0f;

        private void GetCone()
        {
            if (CanGrowCone())
            {
                GrowCone();
                
                Vector3 mousePosition = Camera.ScreenToWorldPoint(Input.mousePosition).SetZ(0f);
                Vector3 direction = mousePosition - transform.position;
                direction = direction.normalized;

                Vector3 left = direction.RotateInPlane(-_coneAngle / 2) * _cone + transform.position;
                Vector3 right = direction.RotateInPlane(_coneAngle / 2) * _cone + transform.position;

                DrawCone(left, right);
            }
            else if (MustStopCone())
            {
                _cone = 0f;
                HideCone();
            }
        }

        private bool CanGrowCone()
        {
            return Input.GetMouseButton(0);
        }

        private void GrowCone()
        {
            if (_cone == 0f)
            {
                _cone = _coneMinimumSize;
            }
            else if (_cone < _coneMaximumSize)
            {
                _cone += _coneGrowthFactor * Time.deltaTime;
            }
        }

        private bool MustStopCone()
        {
            return Input.GetMouseButtonUp(0);
        }

        private void DrawCone(Vector3 left, Vector3 right)
        {
            if (_left != null)
            {
                _left.positionCount = 2;
                _left.SetPositions(new Vector3[] { transform.position, left });
            }

            if (_right != null)
            {
                _right.positionCount = 2;
                _right.SetPositions(new Vector3[] { transform.position, right });
            }

            if (_joint != null)
            {
                _joint.positionCount = 2;
                _joint.SetPositions(new Vector3[] { left, right });
            }
        }

        private void HideCone()
        {
            if (_right != null)
            {
                _right.positionCount = 0;
            }
            if (_left != null)
            {
                _left.positionCount = 0;
            }
            if (_joint != null)
            {
                _joint.positionCount = 0;
            }
        }
        #endregion Cone
    }
}