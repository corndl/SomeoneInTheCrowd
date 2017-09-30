using SITC.Tools;
using UnityEngine;

namespace SITC.Controls
{
    [RequireComponent(typeof(Camera))]
    public class FollowPlayer : SitcBehaviour
    {
        #region Members
        [SerializeField]
        private float _damping = 0f;
        [SerializeField]
        private Vector2 _confinementZone = new Vector2(160f, 90f);
        #endregion Members

        #region Private members
        private InputHandler _player = null;
        private Vector3 _velocity = Vector3.zero;
        #endregion Private members

        #region Getters
        private InputHandler Player { get { _player = _player ?? FindObjectOfType<InputHandler>(); return _player; } }
        #endregion Getters

        #region Lifecycle
        protected override void DoUpdate()
        {
            base.DoUpdate();
            FollowTarget();            
        }

        private void OnDrawGizmos()
        {
            Vector3 topLeft= new Vector3(- _confinementZone.x / 2, _confinementZone.y / 2);
            Vector3 bottomLeft = new Vector3(- _confinementZone.x / 2, - _confinementZone.y / 2);
            Vector3 topRight = new Vector3(_confinementZone.x / 2, _confinementZone.y / 2);
            Vector3 bottomRight = new Vector3(_confinementZone.x / 2, - _confinementZone.y / 2);

            Gizmos.color = Color.red;
            Gizmos.DrawLine(topLeft, topRight);
            Gizmos.DrawLine(topRight, bottomRight);
            Gizmos.DrawLine(bottomRight, bottomLeft);
            Gizmos.DrawLine(bottomLeft, topLeft);
        }
        #endregion Lifecycle

        #region Follow player
        private void FollowTarget()
        {
            if (! CanMove())
            {
                return;
            }

            LerpPosition();
        }

        private bool CanMove()
        {
            if (Player == null)
            {
                return false;
            }
            
            return true;
        }

        private void LerpPosition()
        {
            Vector3 damped = Vector3.SmoothDamp(transform.position.SetZ(0f), Player.transform.position.SetZ(0f), ref _velocity, _damping);
            damped = Confine(damped);
            transform.position = damped.SetZ(transform.position.z);
        }

        private Vector3 Confine(Vector3 damped)
        {
            if (damped.x > _confinementZone.x / 2)
            {
                damped.x = _confinementZone.x / 2;
            }
            else if (damped.x < - _confinementZone.x / 2)
            {
                damped.x = - _confinementZone.x / 2;
            }

            if (damped.y > _confinementZone.y / 2)
            {
                damped.y = _confinementZone.y / 2;
            }
            else if (damped.y < - _confinementZone.y / 2)
            {
                damped.y = - _confinementZone.y / 2;
            }

            return damped;
        }
        #endregion Follow player
    }
}