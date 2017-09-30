using SITC.Tools;
using System.Collections.Generic;
using UnityEngine;

namespace SITC.AI
{
    public class AiPatrolPoints : Singleton<AiPatrolPoints>
    {
        #region Members
        [Header("Patrol"), SerializeField]
        private float _nextDecisionRange = 3f;
        [SerializeField]
        private List<Transform> _positions = null;
        #endregion Members

        #region API
        public static Transform GetNextTarget(Vector3 position)
        {
            if (Instance == null)
            {
                return null;
            }

            List<Transform> positionsInRange = new List<Transform>(Instance._positions);
            positionsInRange.RemoveAll(p => p == null || Vector3.Distance(p.position, position) > Instance._nextDecisionRange);

            if (positionsInRange.Count == 0)
            {
                return null;
            }

            int rand = Random.Range(0, positionsInRange.Count);
            return positionsInRange[rand];
        }
        #endregion API

        #region Debug
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = AiConfiguration.AiTargetGizmoColor;
            _positions.ForEach(DrawTarget);
            Gizmos.color = AiConfiguration.AiTargetLinkGizmoColor;
        }

        private void DrawTarget(Transform target)
        {
            if (target == null)
            {
                return;
            }
            
            Gizmos.DrawSphere(target.position, AiConfiguration.AiTargetGizmoRadius);
        }
        #endregion Debug
    }
}