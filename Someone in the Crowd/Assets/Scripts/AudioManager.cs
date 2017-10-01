using SITC.Tools;
using UnityEngine;

namespace SITC.Audio
{
    public class AudioManager : Singleton<AudioManager>
    {
        #region Members
        [SerializeField]
        private AudioSource _defeat = null;
        [SerializeField]
        private AudioSource _victory = null;
        #endregion Members

        #region Lifecycle
        protected override void Init()
        {
            base.Init();
            DontDestroyOnLoad(gameObject);   
        }
        #endregion Lifecycle

        #region API
        public static void GameOver(bool victory)
        {
            if (Instance == null)
            {
                return;
            }

            if (victory
                && ! Instance._victory.isPlaying)
            {
                Instance._victory.Play();
            }
            else
            {
                Instance._defeat.Play();
            }
        }
        #endregion API
    }
}