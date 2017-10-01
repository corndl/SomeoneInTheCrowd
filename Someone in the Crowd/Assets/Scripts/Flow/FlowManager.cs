using SITC.Tools;
using System.Collections;
using UnityEngine;

namespace SITC
{
    public class FlowManager : Singleton<FlowManager>
    {
        #region Members
        [SerializeField]
        private Canvas _menu = null;
        [SerializeField]
        private Canvas _gameOver = null;
        [SerializeField]
        private float _gameOverScreenDuration = 2f;
        #endregion Members

        #region Private members
        private bool _inMenu = false;
        #endregion Private members

        #region Lifecycle
        protected override void Init()
        {
            base.Init();
            DontDestroyOnLoad(gameObject);
        }

        protected override void DoUpdate()
        {
            base.DoUpdate();

            if (_inMenu
                && Input.anyKeyDown)
            {
                StartGame();
            }
        }
        #endregion Lifecycle

        #region API
        public void StartGame()
        {
            _inMenu = false;
            Instance._menu.gameObject.SetActive(false);
            Instance._gameOver.gameObject.SetActive(false);
        }

        public static void GameOver()
        {
            Instance._inMenu = false;
            Debug.LogError("GAME OVER");
            Instance.StartCoroutine(Instance.GameOverRoutine());
        }

        private IEnumerator GameOverRoutine()
        {
            Instance._gameOver.gameObject.SetActive(true);
            yield return new WaitForSeconds(Instance._gameOverScreenDuration);
            Instance._menu.gameObject.SetActive(true);
            Instance._gameOver.gameObject.SetActive(false);
            Instance._inMenu = true;
        }
        #endregion API
    }
}