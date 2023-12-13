using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Nano.UI
{
    public class SimpleScreen : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] float menuFadeDuration = .3f;

        [Header("Setup - Do not modify")]
        [SerializeField] InputAction hideInput;
        [SerializeField] CanvasGroup CanvasGroup;
        [SerializeField] Button quitButton;
        [SerializeField] bool useTweening = true;

        Action onHideScreenCallback;
        bool isShown = false;

        public void ShowScreen(Action onHideCallback)
        {
            hideInput.Enable();

            quitButton.onClick.AddListener(Hide);

            onHideScreenCallback = onHideCallback;

            hideInput.performed += OnReturnButtonPressed;

            CanvasGroup.blocksRaycasts = true;
            CanvasGroup.interactable = true;
            if (useTweening)
                CanvasGroup.DOFade(1, menuFadeDuration);
            else
                CanvasGroup.alpha = 1;

            isShown = true;
        }

        private void OnReturnButtonPressed(InputAction.CallbackContext obj)
        {
            if (obj.performed)
                Hide();
        }

        private void Hide()
        {
            if (!isShown)
                return;

            isShown = false;
            hideInput.performed -= OnReturnButtonPressed;

            quitButton.onClick.RemoveListener(Hide);
            onHideScreenCallback?.Invoke();

            CanvasGroup.blocksRaycasts = true;
            CanvasGroup.interactable = true;

            if (useTweening)
                CanvasGroup.DOFade(0, menuFadeDuration);
            else
                CanvasGroup.alpha = 0;
        }
    }
}