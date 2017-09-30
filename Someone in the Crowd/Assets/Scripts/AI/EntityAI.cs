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
        private List<Transform> _positions = null;
        #endregion Members

        #region Private members
        private Entity _entity = null;
        private int _currentTarget = 0;
        #endregion Private members

        #region Getters
        private Entity Entity { get { _entity = _entity ?? GetComponent<Entity>(); return _entity; } }
        #endregion Getters

        #region Lifecycle
        protected override void DoUpdate()
        {
            base.DoUpdate();
            Pathfinding();
        }
        #endregion Lifecycle

        #region Pathfinding
        private void Pathfinding()
        {
            if (_positions.Count == 0)
            {
                return;
            }

            if (ReachedCurrentTarget())
            {
                IncrementCurrentTarget();
            }

            MoveTowardsTarget();
        }

        private bool ReachedCurrentTarget()
        {
            return Vector3.Distance(transform.position, _positions[_currentTarget].position) <= AiConfiguration.TargetReachedDistance;
        }

        private void IncrementCurrentTarget()
        {
            ++_currentTarget;

            if (_currentTarget >= _positions.Count)
            {
                _currentTarget = 0;
            }
        }

        private void MoveTowardsTarget()
        {
            Entity.Move(_positions[_currentTarget].position - transform.position);
        }
        #endregion Pathfinding

        #region Debug
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = AiConfiguration.AiTargetGizmoColor;
            _positions.ForEach(DrawTarget);
        }

        private void DrawTarget(Transform target)
        {
            if (target == null)
            {
                return;
            }

            string targetText = _positions.IndexOf(target).ToString();
            Gizmos.DrawSphere(target.position, AiConfiguration.AiTargetGizmoRadius);
            UnityEditor.Handles.Label(target.position, targetText, AiConfiguration.BoldStyle);
        }
        #endregion Debug
    }
}