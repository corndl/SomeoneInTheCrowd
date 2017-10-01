using SITC.Tools;
using System.Collections.Generic;
using UnityEngine;

namespace SITC.Audio
{
    public class AudioManager : Singleton<AudioManager>
    {
        #region Members
        [SerializeField]
        private AudioSource _crowd = null;
        [SerializeField]
        private AudioSource _defeat = null;
        [SerializeField]
        private AudioSource _victory = null;
        [SerializeField]
        private List<AudioSource> _alert = null;
        [SerializeField]
        private List<AudioSource> _alerted = null;
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

            Instance._crowd.Stop();

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

        public static void StartGame()
        {
            if (Instance != null)
            {
                Instance._crowd.Play();
            }
        }

        public static void Alert()
        {
            if (Instance != null)
            {
                Instance._alert.GetRandom().Play();
            }
        }

        public static void Alerted()
        {
            if (Instance != null)
            {
                Instance._alerted.GetRandom().Play();
            }
        }
        #endregion API
    }
}