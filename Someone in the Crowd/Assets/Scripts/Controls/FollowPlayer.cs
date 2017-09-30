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
        #endregion Lifecycle

        #region Follow player
        private void FollowTarget()
        {
            if (! MustMove())
            {
                return;
            }

            LerpPosition();
        }

        private bool MustMove()
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
            transform.position = damped.SetZ(transform.position.z);
        }
        #endregion Follow player
    }
}