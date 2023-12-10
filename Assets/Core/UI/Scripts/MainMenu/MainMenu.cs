using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;

namespace Nano.UI
{
    public class MainMenu : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] float menuFadeDuration = .3f;

        [Header("Setup - Do not modify")]
        [SerializeField] Button startButton;
        [SerializeField] Button tutorialButton;
        [SerializeField] Button optionsButton;
        [SerializeField] Button quitButton;
        [SerializeField] CanvasGroup mainMenuGroup;
        [SerializeField] TutorialScreen tutorialScreen;

        public static event Action OnStartButtonClicked;

        void Awake()
        {
            startButton.onClick.AddListener(StartButton);
            tutorialButton.onClick.AddListener(TutorialButton);
            optionsButton.onClick.AddListener(OptionsButton);
            quitButton.onClick.AddListener(QuitButton);
        }

        private void OnDestroy()
        {
            startButton.onClick.RemoveListener(StartButton);
            tutorialButton.onClick.RemoveListener(TutorialButton);
            optionsButton.onClick.RemoveListener(OptionsButton);
            quitButton.onClick.RemoveListener(QuitButton);
        }

        private void StartButton()
        {
            OnStartButtonClicked?.Invoke();
        }

        private void TutorialButton()
        {
            HideMainMenu();
            tutorialScreen.ShowTutorialScreen(ShowMainMenu);
        }

        private void OptionsButton()
        {
            HideMainMenu();
        }

        private void QuitButton()
        {
            Debug.Log("Quitting app.");

            Application.Quit();
        }

        void ShowMainMenu()
        {
            mainMenuGroup.DOFade(1, menuFadeDuration);
        }

        void HideMainMenu()
        {
            mainMenuGroup.DOFade(0, menuFadeDuration);
        }
    }
}