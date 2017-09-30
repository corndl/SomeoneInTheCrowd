using SITC.Tools;
using System.Collections.Generic;
using UnityEngine;

namespace SITC.AI
{
    [RequireComponent(typeof(Entity))]
    public class EntityAI : SitcBehaviour
    {
        #region Members
        [SerializeField]
        private float _alertDuration = 2f;
        #endregion Members

        #region Private members
        private Entity _entity = null;
        private Transform _target = null;
        private float _targetReachedTime = 0f;
        private float _delayBeforeNextTarget = 0f;
        private float _alertTime = 0f;
        private float _currentSpeed = 1f;
        #endregion Private members

        #region Getters
        private Entity Entity { get { _entity = _entity ?? GetComponent<Entity>(); return _entity; } }
        #endregion Getters

        #region Lifecycle
        protected override void DoUpdate()
        {
            base.DoUpdate();

            if (! CheckAlert())
            {
                Pathfinding();
            }
        }
        #endregion Lifecycle

        #region API
        public void Alert()
        {
            Debug.Log(name + " was alerted");
            Entity.Alert();
            _alertTime = Time.time;
        }
        #endregion API

        #region Pathfinding
        private void Pathfinding()
        {
            if (_targetReachedTime + _delayBeforeNextTarget > Time.time)
            {
                return;
            }

            if (ReachedCurrentTarget())
            {
                _targetReachedTime = Time.time;
                _currentSpeed = Random.Range(AiConfiguration.MinimumSpeedRatio, 1f);
                _delayBeforeNextTarget = Random.Range(0f, AiConfiguration.MaxDelayBeforeNextTarget);
                _target = AiPatrolPoints.GetNextTarget(transform.position, Entity.GetConviction());
            }

            MoveTowardsTarget();
        }

        private bool ReachedCurrentTarget()
        {
            if (_target == null)
            {
                return true;
            }

            return Vector3.Distance(transform.position, _target.position) <= AiConfiguration.TargetReachedDistance;
        }

        private void MoveTowardsTarget()
        {
            if (_target == null)
            {
                return;
            }

            Vector3 direction = (_target.position - transform.position).normalized * _currentSpeed;
            Entity.Move(direction);
        }
        #endregion Pathfinding

        #region Alert
        private bool CheckAlert()
        {
            return _alertTime + _alertDuration > Time.time;
        }
        #endregion Alert
    }
}