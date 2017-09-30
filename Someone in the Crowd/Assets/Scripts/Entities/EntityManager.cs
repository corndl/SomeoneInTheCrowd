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

            float rand = 0f;

            foreach (var entity in Entities)
            {
                rand = Random.Range(0f, 1f);
                float conviction = EntityConfiguration.ConvictionCurve.Evaluate(rand);
                conviction = Mathf.Clamp(conviction, -1f, 1f);
                entity.SetConviction(conviction);
            }
        }
        #endregion Lifecycle
    }
}