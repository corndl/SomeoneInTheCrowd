using SITC.Tools;
using System.Collections.Generic;
using UnityEngine;

namespace SITC.AI
{
    public class AiPatrolPoints : Singleton<AiPatrolPoints>
    {
        #region Members
        [Header("Patrol"), SerializeField]
        private AnimationCurve _nextDecisionRange = null;
        [SerializeField]
        private List<Transform> _positions = null;
        #endregion Members

        #region Lifecycle
        protected override void Init()
        {
            base.Init();
            InitPositions();
        }

        private void OnValidate()
        {
            InitPositions();
        }
        #endregion Lifecycle

        #region API
        public void InitPositions()
        {
            _positions = new List<Transform>();

            for (int i = 0, count = transform.childCount; i < count; ++i)
            {
                _positions.Add(transform.GetChild(i));
            }
        }

        public static Transform GetNextTarget(Vector3 position, float conviction)
        {
            if (Instance == null)
            {
                return null;
            }

            List<Transform> positionsInRange = new List<Transform>(Instance._positions);
            float radius = Instance._nextDecisionRange.Evaluate(conviction);
            positionsInRange.RemoveAll(p => p == null || Vector3.Distance(p.position, position) > radius);

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
            InitPositions();
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