namespace SITC.Tools
{
    public class Singleton<T> : SitcBehaviour where T : SitcBehaviour
    {
        public static T Instance = null;

        #region Lifecycle
        protected override void Init()
        {
            base.Init();
            if (Instance == null)
            {
                Instance = this as T;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        protected override void DoDestroy()
        {
            base.DoDestroy();
            if (Instance == this)
            {
                Instance = null;
            }
        }
        #endregion Lifecycle
    }
}