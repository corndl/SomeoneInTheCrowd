namespace SITC.Tools
{
    public class Singleton<T> : SitcBehaviour where T : SitcBehaviour
    {
        private static T _instance = null;

        public static T Instance { get { _instance = _instance ?? FindObjectOfType<T>(); return _instance; } }

        #region Lifecycle
        protected override void Init()
        {
            base.Init();
            if (Instance == null)
            {
                _instance = this as T;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
        }

        protected override void DoDestroy()
        {
            base.DoDestroy();
            if (Instance == this)
            {
                _instance = null;
            }
        }
        #endregion Lifecycle
    }
}