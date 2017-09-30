using SITC.Tools;
using System.Collections.Generic;
using UnityEngine;

namespace SITC.AI
{
    [RequireComponent(typeof(Entity))]
    public class EntityAI : SitcBehaviour
    {
        #region Data structures
        public enum EAIState
        {
            RoamingPatrol,
            OppressionGoToEntity,
            OppressionTakeAwayEntity,
            OppressedTakenAway,
            Witness
        }
        #endregion Data structures

        #region Members
        [SerializeField]
        private float _witnessDuration = 2f;
        #endregion Members

        #region Private members
        private Entity _entity = null;
        private EAIState _currentState = EAIState.RoamingPatrol;
        private Transform _target = null;
        private float _targetReachedTime = 0f;
        private float _delayBeforeNextTarget = 0f;
        private float _witnessTime = 0f;
        private float _currentSpeed = 1f;
        private bool _oppressor = false;
        #endregion Private members

        #region Getters
        private Entity Entity { get { _entity = _entity ?? GetComponent<Entity>(); return _entity; } }
        #endregion Getters

        #region Lifecycle
        protected override void DoUpdate()
        {
            base.DoUpdate();

            if (! _oppressor
                && Entity.GetConviction() == -1f)
            {
                _oppressor = true;
                _currentState = EAIState.OppressionGoToEntity;
            }

            switch (_currentState)
            {
                case EAIState.RoamingPatrol:
                    Pathfinding();
                    break;

                case EAIState.Witness:
                    if (! CheckWitness())
                    {
                        _currentState = EAIState.RoamingPatrol;
                    }
                    Pathfinding();
                    break;
            }

            if (! CheckWitness())
            {
                Pathfinding();
            }
        }
        #endregion Lifecycle

        #region API
        public void Alert(float conviction)
        {
            if (_currentState != EAIState.Witness)
            {
                return;
            }

            Debug.Log(name + " was alerted");
            Entity.AddConviction(conviction);
        }

        public void SetWitness()
        {
            _witnessTime = Time.time;
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
        private bool CheckWitness()
        {
            return _witnessTime + _witnessDuration > Time.time;
        }
        #endregion Alert
    }
}