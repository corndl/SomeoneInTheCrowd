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
        }
        #endregion Lifecycle
        
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

                entity.SetConviction(conviction + variation);
            }
        }
        #endregion Conviction
    }
}