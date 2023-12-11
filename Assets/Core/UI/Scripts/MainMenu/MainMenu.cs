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
        [SerializeField] Button quitButton;
        [SerializeField] CanvasGroup mainMenuGroup;
        [SerializeField] TutorialScreen tutorialScreen;
        [SerializeField] OptionsMenu optionsScreen;
        [SerializeField] List<Selectable> availableButtons;

        Selectable selectedOption;

        bool isMoving = false;

        public static event Action OnStartButtonClicked;

        void Awake()
        {
            startButton.onClick.AddListener(StartButton);
            tutorialButton.onClick.AddListener(TutorialButton);
            optionsButton.onClick.AddListener(OptionsButton);
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
            quitButton.onClick.RemoveListener(QuitButton);
        }

        private void StartButton()
        {
            OnStartButtonClicked?.Invoke();
            HideMainMenu();
        }

        private void TutorialButton()
        {
            HideMainMenu();
            tutorialScreen.ShowTutorialScreen(ShowMainMenu);
        }

        private void OptionsButton()
        {
            HideMainMenu();
            optionsScreen.ShowOptionsScreen(ShowMainMenu);
        }

        private void QuitButton()
        {
            Debug.Log("Quitting app.");

            Application.Quit();
        }

        void ShowMainMenu()
        {
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