using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Nano.UI
{
    public class PauseMenu : MonoBehaviour
    {
        [Header("Setup - Do not modify")]
        [SerializeField] InputAction directionalInput;
        [SerializeField] Button resumeButton;
        [SerializeField] Button tutorialButton;
        [SerializeField] Button optionsButton;
        [SerializeField] Button returnToTitleButton;
        [SerializeField] Button quitButton;
        [SerializeField] CanvasGroup pauseMenuGroup;
        [SerializeField] TutorialScreen tutorialScreen;
        [SerializeField] OptionsMenu optionsScreen;
        [SerializeField] List<Selectable> availableButtons;

        Selectable selectedOption;

        bool isMoving = false;
        bool isMenuShown = false;

        public static event Action onResumeButtonClicked;
        public static event Action onReturnToTitleButtonClicked;

        void Awake()
        {
            resumeButton.onClick.AddListener(ResumeButton);
            tutorialButton.onClick.AddListener(TutorialButton);
            optionsButton.onClick.AddListener(OptionsButton);
            returnToTitleButton.onClick.AddListener(ReturnToTitleButton);
            quitButton.onClick.AddListener(QuitButton);
            optionsScreen.CheckPlayerPrefsInitiated();
            SelectNewOption(0);
        }

        private void OnDestroy()
        {
            resumeButton.onClick.RemoveListener(ResumeButton);
            tutorialButton.onClick.RemoveListener(TutorialButton);
            optionsButton.onClick.RemoveListener(OptionsButton);
            returnToTitleButton.onClick.RemoveListener(ReturnToTitleButton);
            quitButton.onClick.RemoveListener(QuitButton);
        }

        private void ResumeButton()
        {
            if (!isMenuShown)
                return;
            HidePauseMenu();
            onResumeButtonClicked?.Invoke();
        }

        private void TutorialButton()
        {
            if (!isMenuShown)
                return;
            HidePauseMenu();
            tutorialScreen.ShowTutorialScreen(ShowPauseMenu);
        }

        private void OptionsButton()
        {
            if (!isMenuShown)
                return;
            HidePauseMenu();
            optionsScreen.ShowOptionsScreen(ShowPauseMenu);
        }

        private void ReturnToTitleButton()
        {
            if (!isMenuShown)
                return;
            HidePauseMenu();
            onReturnToTitleButtonClicked?.Invoke();
        }

        private void QuitButton()
        {
            if (!isMenuShown)
                return;
            Debug.Log("Quitting app.");

            Application.Quit();
        }

        public void ShowPauseMenu()
        {
            isMenuShown = true;
            directionalInput.Enable();
            directionalInput.started += OnDirectionalInput;
            directionalInput.canceled += OnDirectionalInputCanceled;
            pauseMenuGroup.alpha = 1;
            pauseMenuGroup.blocksRaycasts = true;
            pauseMenuGroup.interactable = true;
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

        public void HidePauseMenu()
        {
            isMenuShown = false;
            pauseMenuGroup.alpha = 0;
            pauseMenuGroup.blocksRaycasts = false;
            pauseMenuGroup.interactable = false;
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