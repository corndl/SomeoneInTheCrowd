using UnityEngine;

namespace SITC.Tools
{
    public class SitcBehaviour : MonoBehaviour
    {
        #region Lifecycle
        protected void Awake()
        {

        }

        protected void Start()
        {
            Init();
        }

        protected void Update()
        {
            DoUpdate();
        }

        protected void OnDestroy()
        {
            DoDestroy();
        }

        protected virtual void Init()
        {

        }

        protected virtual void DoUpdate()
        {

        }

        protected virtual void DoDestroy()
        {

        }
        #endregion Lifecycle
    }
}