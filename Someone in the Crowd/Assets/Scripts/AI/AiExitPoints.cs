using SITC.Tools;
using System.Collections.Generic;
using UnityEngine;

namespace SITC.AI
{
    public class AiExitPoints : Singleton<AiExitPoints>
    {
        #region Members
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

        public static Transform GetClosestExit(Vector3 position)
        {
            if (Instance == null)
            {
                return null;
            }

            float smallestDistance = float.MaxValue;
            Transform target = null;

            foreach (var t in Instance._positions)
            {
                if (Vector3.Distance(position, t.position) < smallestDistance)
                {
                    smallestDistance = Vector3.Distance(position, t.position);
                    target = t;
                }
            }

            return target;
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