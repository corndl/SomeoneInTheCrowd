using SITC.AI;
using SITC.Controls;
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
        private List<Entity> _takenAway = null;
        #endregion Members

        #region Getters
        private List<Entity> Entities { get { _entities = _entities ?? FindObjectsOfType<Entity>().ToList(); return _entities; } }
        private List<Entity> TakenAway { get { _takenAway = _takenAway ?? new List<Entity>(); return _takenAway; } }
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
            if (Instance == null)
            {
                return null;
            }

            List<Entity> potentials = new List<Entity>(Instance.Entities);
            potentials.RemoveAll(e => Vector3.Distance(oppressor.transform.position, e.transform.position) > AiConfiguration.SearchResistantRange);
            potentials.RemoveAll(e => Instance.TakenAway.Contains(e));

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

            Instance.TakenAway.Add(target);
            return target;
        }
        
        public static void TakeAway(Entity oppressor, Entity oppressed)
        {
            Debug.Log(oppressor.name + " took away " + oppressed.name + " !");
            
            if (oppressed.GetComponent<InputHandler>() != null)
            {
                Instance.Entities.ForEach(e => e.Stop());
                FlowManager.GameOver();
            }

            if (Instance == null)
            {
                return;
            }

            List<Entity> entities = new List<Entity>(Instance.Entities);
            entities.Remove(oppressed);
            entities.Remove(oppressor);

            foreach (var entity in entities)
            {
                float range = AiConfiguration.WitnessRange.Evaluate(entity.GetConviction());
                EntityAI ai = entity.GetComponent<EntityAI>();

                if (ai != null
                    && Vector3.Distance(entity.transform.position, oppressed.transform.position) < range)
                {
                    float duration = AiConfiguration.WitnessDuration.Evaluate(entity.GetConviction());
                    ai.SetWitness(duration);
                }
            }
        }

        public static void RemoveTaken(Entity entity)
        {
            if (Instance != null)
            {
                Instance.TakenAway.Remove(entity); 
            }
        }

        public static float[] GetRatios()
        {
            float[] ratios = new float[3];

            if (Instance == null
                || Instance.Entities.Count == 0)
            {
                return ratios;
            }

            Instance.Entities.ForEach(
                (e) =>
                {
                    if (e.GetConviction() < EntityConfiguration.MinimumConvictionForOppressor)
                    {
                        ratios[0] += 1;
                    }
                    else if (e.GetConviction() > EntityConfiguration.MinimumConvictionForResistant)
                    {
                        ratios[2] += 1;
                    }
                    else
                    {
                        ratios[1] += 1;
                    }
                }
            );

            ratios[0] *= 100f / Instance.Entities.Count;
            ratios[1] *= 100f / Instance.Entities.Count;
            ratios[2] *= 100f / Instance.Entities.Count;

            return ratios;
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