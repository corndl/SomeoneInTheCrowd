using SITC.Tools;
using UnityEngine;

namespace SITC.Audio
{
    public class AudioManager : Singleton<AudioManager>
    {
        #region Members
        [SerializeField]
        private AudioSource _music = null;
        #endregion Members

        #region Lifecycle
        protected override void Init()
        {
            base.Init();
            DontDestroyOnLoad(gameObject);
            
        }
        #endregion Lifecycle
    }
}