using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Nano.UI
{
    public class TutorialScreen : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] float menuFadeDuration = .3f;

        [Header("Setup - Do not modify")]
        [SerializeField] InputAction hideTutorialInput;
        [SerializeField] CanvasGroup tutorialCanvasGroup;
        [SerializeField] Button quitTutorialButton;
        [SerializeField] bool useTweening = true;

        Action onHideTutorialScreenCallback;
        bool isTutorialShown = false;

        public void ShowTutorialScreen(Action onHideTutorialCallback)
        {
            hideTutorialInput.Enable();

            quitTutorialButton.onClick.AddListener(HideTutorial);

            onHideTutorialScreenCallback = onHideTutorialCallback;

            hideTutorialInput.performed += OnReturnButtonPressed;

            tutorialCanvasGroup.blocksRaycasts = true;
            tutorialCanvasGroup.interactable = true;
            if (useTweening)
                tutorialCanvasGroup.DOFade(1, menuFadeDuration);
            else
                tutorialCanvasGroup.alpha = 1;

            isTutorialShown = true;
        }

        private void OnReturnButtonPressed(InputAction.CallbackContext obj)
        {
            if (obj.performed)
                HideTutorial();
        }

        private void HideTutorial()
        {
            if (!isTutorialShown)
                return;

            isTutorialShown = false;
            hideTutorialInput.performed -= OnReturnButtonPressed;

            quitTutorialButton.onClick.RemoveListener(HideTutorial);
            onHideTutorialScreenCallback?.Invoke();

            tutorialCanvasGroup.blocksRaycasts = true;
            tutorialCanvasGroup.interactable = true;

            if (useTweening)
                tutorialCanvasGroup.DOFade(0, menuFadeDuration);
            else
                tutorialCanvasGroup.alpha = 0;
        }
    }
}