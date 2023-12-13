using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Nano.UI
{
    public class OptionsMenu : MonoBehaviour
    {
        const string MASTER_VOLUME_KEY = "masterVolume";
        const string BGM_VOLUME_KEY = "BGMVolume";
        const string SFX_VOLUME_KEY = "SoundEffectsVolume";
        const string BACKGROUND_ANIMATIONS_DISABLED_KEY = "SFXVolume";

        [Header("Settings")]
        [SerializeField] float menuFadeDuration = .3f;

        [Header("Setup - Do not modify")]
        [SerializeField] InputAction hideOptionsInput;
        [SerializeField] CanvasGroup optionsCanvasGroup;
        [SerializeField] Button quitOptionsButton;
        [SerializeField] SliderHandler masterVolumeSlider;
        [SerializeField] SliderHandler bgmVolumeSlider;
        [SerializeField] SliderHandler sfxVolumeSlider;
        [SerializeField] ToggleHandler backgroundEnabledToggle;
        [SerializeField] bool useTweening = true;

        [SerializeField] AK.Wwise.Event UiMenuBack_00_SFX;

        Action onHideOptionsScreenCallback;
        bool isOptionsShown = false;

        public void ShowOptionsScreen(Action onHideTutorialCallback)
        {
            hideOptionsInput.Enable();
            
            CheckPlayerPrefsInitiated();

            masterVolumeSlider.Init(MASTER_VOLUME_KEY);
            bgmVolumeSlider.Init(BGM_VOLUME_KEY);
            sfxVolumeSlider.Init(SFX_VOLUME_KEY);
            backgroundEnabledToggle.Init(BACKGROUND_ANIMATIONS_DISABLED_KEY);

            quitOptionsButton.onClick.AddListener(HideOptions);

            onHideOptionsScreenCallback = onHideTutorialCallback;

            hideOptionsInput.performed += OnReturnButtonPressed;

            optionsCanvasGroup.blocksRaycasts = true;
            optionsCanvasGroup.interactable = true;

            if (useTweening)
                optionsCanvasGroup.DOFade(1, menuFadeDuration);
            else
                optionsCanvasGroup.alpha = 1;

            isOptionsShown = true;
            masterVolumeSlider.Select();
        }

        private void OnReturnButtonPressed(InputAction.CallbackContext obj)
        {
            if (obj.performed)
                HideOptions();
                UiMenuBack_00_SFX.Post(gameObject);
        }

        private void HideOptions()
        {
            if (!isOptionsShown)
                return;

            masterVolumeSlider.OnOptionsHidden();
            bgmVolumeSlider.OnOptionsHidden();
            sfxVolumeSlider.OnOptionsHidden();
            backgroundEnabledToggle.OnOptionsHidden();

            isOptionsShown = false;
            hideOptionsInput.performed -= OnReturnButtonPressed;

            quitOptionsButton.onClick.RemoveListener(HideOptions);

            onHideOptionsScreenCallback?.Invoke();

            optionsCanvasGroup.blocksRaycasts = false;
            optionsCanvasGroup.interactable = false;

            if (useTweening)
                optionsCanvasGroup.DOFade(0, menuFadeDuration);
            else
                optionsCanvasGroup.alpha = 0;
        }

        public void CheckPlayerPrefsInitiated()
        {
            if (!PlayerPrefs.HasKey(MASTER_VOLUME_KEY))
                PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, 1);

            if (!PlayerPrefs.HasKey(BGM_VOLUME_KEY))
                PlayerPrefs.SetFloat(BGM_VOLUME_KEY, 1);

            if (!PlayerPrefs.HasKey(SFX_VOLUME_KEY))
                PlayerPrefs.SetFloat(SFX_VOLUME_KEY, 1);

            if (!PlayerPrefs.HasKey(BACKGROUND_ANIMATIONS_DISABLED_KEY))
                PlayerPrefs.SetInt(BACKGROUND_ANIMATIONS_DISABLED_KEY, 0);
        }
    }
}