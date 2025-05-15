using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using BaseGame.Scripts.UI;

namespace BaseGame.Scripts.Level
{
    public class RestartOnGameOver
    {
        private readonly EndGame _winScreen;
        private readonly EndGame _loseScreen;
        private readonly Button _restartButton;

        public RestartOnGameOver(EndGame winScreen, EndGame loseScreen, Button restartButton)
        {
            _winScreen = winScreen ?? throw new ArgumentNullException(nameof(winScreen));
            _loseScreen = loseScreen ?? throw new ArgumentNullException(nameof(loseScreen));
            _restartButton = restartButton ?? throw new ArgumentNullException(nameof(restartButton));

            _restartButton.gameObject.SetActive(false);

            _winScreen.Shown += ShowRestart;
            _loseScreen.Shown += ShowRestart;

            _restartButton.onClick.AddListener(RestartScene);
        }

        private void ShowRestart()
        {
            _restartButton.gameObject.SetActive(true);
        }

        private void RestartScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}