using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Sirenix.OdinInspector;
using Nano.UI;

namespace Nano.Managers
{
    public class SceneHandler : MonoBehaviour
    {
        public static SceneHandler Instance;

        [SerializeField] string menuScene;
        [SerializeField] string gameScene;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);
            Instance = this;

            MainMenu.OnStartButtonClicked += SwitchToGameScene;
            PauseMenu.onReturnToTitleButtonClicked += SwitchToMenuScene;
            GameOverScreen.onRestart += SwitchToGameScene;
            GameOverScreen.onTitle += SwitchToMenuScene;
        }

        private void OnDestroy()
        {
            MainMenu.OnStartButtonClicked -= SwitchToGameScene;
            PauseMenu.onReturnToTitleButtonClicked -= SwitchToMenuScene;
            GameOverScreen.onRestart -= SwitchToGameScene;
            GameOverScreen.onTitle -= SwitchToMenuScene;
        }

        public void SwitchToGameScene()
        {
            SceneManager.LoadScene(gameScene);
        }

        public void SwitchToMenuScene()
        {
            SceneManager.LoadScene(menuScene);
        }
    }
}