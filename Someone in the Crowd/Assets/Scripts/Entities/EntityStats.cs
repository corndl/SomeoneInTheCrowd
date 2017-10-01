using SITC.Tools;
using UnityEngine;
using UnityEngine.UI;

namespace SITC.Entities
{
    public class EntityStats : SitcBehaviour
    {
        #region Members
        [SerializeField]
        private Text _oppressors = null;
        [SerializeField]
        private Text _neutrals = null;
        [SerializeField]
        private Text _resistants = null;
        #endregion Members

        #region Lifecycle
        protected override void DoUpdate()
        {
            base.DoUpdate();

            float[] ratios = EntityManager.GetRatios();

            _oppressors.SetText(((int)ratios[0]).ToString() + "%");
            _neutrals.SetText(((int)ratios[1]).ToString() + "%");
            _resistants.SetText(((int)ratios[2]).ToString() + "%");
        }
        #endregion Lifecycle
    }
}