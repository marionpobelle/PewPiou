using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;
using UnityEngine.InputSystem;

namespace Nano.UI
{
    public class MainMenu : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] float menuFadeDuration = .3f;

        [Header("Setup - Do not modify")]
        [SerializeField] InputAction directionalInput;
        [SerializeField] Button startButton;
        [SerializeField] Button tutorialButton;
        [SerializeField] Button optionsButton;
        [SerializeField] Button creditsButton;
        [SerializeField] Button quitButton;
        [SerializeField] CanvasGroup mainMenuGroup;
        [SerializeField] SimpleScreen tutorialScreen;
        [SerializeField] SimpleScreen creditsScreen;
        [SerializeField] OptionsMenu optionsScreen;
        [SerializeField] List<Selectable> availableButtons;

        Selectable selectedOption;

        bool isMoving = false;
        bool isMenuShown = true;

        public static event Action OnStartButtonClicked;

        void Awake()
        {
            startButton.onClick.AddListener(StartButton);
            tutorialButton.onClick.AddListener(TutorialButton);
            optionsButton.onClick.AddListener(OptionsButton);
            creditsButton.onClick.AddListener(CreditsButton);
            quitButton.onClick.AddListener(QuitButton);
            optionsScreen.CheckPlayerPrefsInitiated();
            ShowMainMenu();
            SelectNewOption(0);
        }

        private void OnDestroy()
        {
            startButton.onClick.RemoveListener(StartButton);
            tutorialButton.onClick.RemoveListener(TutorialButton);
            optionsButton.onClick.RemoveListener(OptionsButton);
            creditsButton.onClick.RemoveListener(CreditsButton);
            quitButton.onClick.RemoveListener(QuitButton);
        }

        private void StartButton()
        {
            if (!isMenuShown)
                return;
            OnStartButtonClicked?.Invoke();
            HideMainMenu();
        }

        private void TutorialButton()
        {
            if (!isMenuShown)
                return;
            HideMainMenu();
            tutorialScreen.ShowScreen(ShowMainMenu);
        }

        private void OptionsButton()
        {
            if (!isMenuShown)
                return;
            HideMainMenu();
            optionsScreen.ShowOptionsScreen(ShowMainMenu);
        }

        private void CreditsButton()
        {
            if (!isMenuShown)
                return;
            HideMainMenu();
            creditsScreen.ShowScreen(ShowMainMenu);
        }

        private void QuitButton()
        {
            if (!isMenuShown)
                return;
            Debug.Log("Quitting app.");

            Application.Quit();
        }

        void ShowMainMenu()
        {
            isMenuShown = true;
            directionalInput.Enable();
            directionalInput.started += OnDirectionalInput;
            directionalInput.canceled += OnDirectionalInputCanceled;
            mainMenuGroup.DOFade(1, menuFadeDuration);
            mainMenuGroup.blocksRaycasts = true;
            mainMenuGroup.interactable = true;
            SelectNewOption(availableButtons.IndexOf(selectedOption));
        }

        private void OnDirectionalInputCanceled(InputAction.CallbackContext obj)
        {
            isMoving = false;
        }

        private void OnDirectionalInput(InputAction.CallbackContext obj)
        {
            if (!isMoving)
            {
                isMoving = true;
                int dir = Mathf.RoundToInt(obj.ReadValue<Vector2>().y);
                SelectNewOption(availableButtons.IndexOf(selectedOption) - dir);
            }
        }

        void HideMainMenu()
        {
            isMenuShown = false;
            mainMenuGroup.DOFade(0, menuFadeDuration);
            mainMenuGroup.blocksRaycasts = false;
            mainMenuGroup.interactable = false;
            directionalInput.started -= OnDirectionalInput;
            directionalInput.canceled -= OnDirectionalInputCanceled;
        }

        void SelectNewOption(int listIndex)
        {
            if (listIndex >= 0 && listIndex < availableButtons.Count)
            {
                selectedOption = availableButtons[listIndex];
                selectedOption.Select();
            }
        }
    }
}