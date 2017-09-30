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
        [SerializeField]
        private List<Transform> _positions = null;
        #endregion Members

        #region Private members
        private Entity _entity = null;
        private int _currentTarget = 0;
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

        #region Alert
        private bool CheckAlert()
        {
            return _alertTime + _alertDuration > Time.time;
        }
        #endregion Alert

        #region Debug
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = AiConfiguration.AiTargetGizmoColor;
            _positions.ForEach(DrawTarget);
            Gizmos.color = AiConfiguration.AiTargetLinkGizmoColor;
            DrawLinks();
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

        private void DrawLinks()
        {
            if (_positions.Count < 2)
            {
                return;
            }

            for (int i = 0, count = _positions.Count; i < count; i++)
            {
                int next = (i + 1 == count)
                    ? 0
                    : i + 1;
                Gizmos.DrawLine(_positions[i].position, _positions[next].position);
            }
        }
        #endregion Debug
    }
}