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
        private float _alertTime = 0f;
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
            if (ReachedCurrentTarget())
            {
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

            Entity.Move(_target.position - transform.position);
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