using SITC.Tools;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SITC.Entities
{
    public class EntityManager : Singleton<EntityManager>
    {
        #region Members
        private List<Entity> _entities = null;
        #endregion Members

        #region Getters
        private List<Entity> Entities { get { _entities = _entities ?? FindObjectsOfType<Entity>().ToList(); return _entities; } }
        #endregion Getters

        #region Lifecycle
        protected override void Init()
        {
            base.Init();
            RandomizeInitialConviction();
        }

        protected override void DoUpdate()
        {
            base.DoUpdate();
            NormalizeConviction();
            InfluenceNeighbours();
        }
        #endregion Lifecycle

        #region API
        public static Entity GetOppressionTarget(Entity oppressor)
        {
            List<Entity> potentials = new List<Entity>(Instance.Entities);
            potentials.RemoveAll(e => Vector3.Distance(oppressor.transform.position, e.transform.position) > AiConfiguration.SearchResistantRange);

            float maxConviction = 0f;
            Entity target = null;

            foreach (var entity in potentials)
            {
                if (entity.GetConviction() > maxConviction)
                {
                    maxConviction = entity.GetConviction();
                    target = entity;
                }
            }

            return target;
        }
        #endregion API

        #region Conviction
        private void RandomizeInitialConviction()
        {
            float rand = 0f;

            foreach (var entity in Entities)
            {
                rand = Random.Range(0f, 1f);
                float conviction = EntityConfiguration.ConvictionInitialRepartition.Evaluate(rand);
                conviction = Mathf.Clamp(conviction, -1f, 1f);
                entity.SetConviction(conviction);
            }
        }
        
        private void NormalizeConviction()
        {
            foreach (var entity in Entities)
            {
                float conviction = entity.GetConviction();
                if (Mathf.Abs(conviction) == 1f
                    || conviction == 0f)
                {
                    continue;
                }

                float variation = EntityConfiguration.ConvictionNormalizationFactor.Evaluate(conviction) * Time.deltaTime;
                variation = (conviction > 0)
                    ? - variation
                    : variation;

                if (Mathf.Abs(variation) > Mathf.Abs(conviction))
                {
                    variation = - conviction;
                }

                entity.AddConviction(variation);
            }
        }

        private void InfluenceNeighbours()
        {
            foreach (var entity in Entities)
            {
                float conviction = entity.GetConviction();
                float radius = EntityConfiguration.InfluenceRadius.Evaluate(conviction);
                float influenceDelta = EntityConfiguration.InfluenceFactor.Evaluate(conviction) * Time.deltaTime;

                if (radius == 0f 
                    || influenceDelta == 0f)
                {
                    continue;
                }

                List<Entity> influenced = new List<Entity>(Entities);
                influenced.RemoveAll(e => Vector3.Distance(entity.transform.position, e.transform.position) > radius);
                influenced.Remove(entity);

                foreach (var inf in influenced)
                {
                    inf.AddConviction(influenceDelta);
                }
            }
        }
        #endregion Conviction
    }
}