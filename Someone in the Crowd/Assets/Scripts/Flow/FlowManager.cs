using SITC.Entities;
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
        [SerializeField]
        private float _cameraFocusOrthoSize = 3f;
        [SerializeField]
        private float _cameraFocusDamping = .3f;
        #endregion Members

        #region Private members
        private bool _inMenu = false;
        private Vector3 _velocity = Vector3.zero;

        private static bool _first = true;
        #endregion Private members

        #region Lifecycle
        protected override void Init()
        {
            base.Init();

            if (_first)
            {
                _first = false;
                DontDestroyOnLoad(gameObject);
                _inMenu = true;
                EntityManager.StopAll();
                Instance._menu.gameObject.SetActive(true);
            }
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
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
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
            Camera c = FindObjectOfType<Camera>();
            Vector3 target = new Vector3(Instance._cameraFocusOrthoSize, 0f, 0f);

            while (Mathf.Abs(c.orthographicSize - Instance._cameraFocusOrthoSize) > 0.1f)
            {
                Vector3 orthosize = new Vector3(c.orthographicSize, 0f, 0f);
                Vector3 damped = Vector3.SmoothDamp(orthosize, target, ref _velocity, _cameraFocusDamping);
                c.orthographicSize = damped.x;
                yield return new WaitForEndOfFrame();
            }

            Instance._gameOver.gameObject.SetActive(true);
            yield return new WaitForSeconds(Instance._gameOverScreenDuration);
            Instance._menu.gameObject.SetActive(true);
            Instance._gameOver.gameObject.SetActive(false);
            Instance._inMenu = true;
        }
        #endregion API
    }
}