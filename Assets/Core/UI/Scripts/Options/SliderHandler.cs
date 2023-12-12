using System;
using UnityEngine;
using UnityEngine.UI;

namespace Nano.UI
{
    public class SliderHandler : MonoBehaviour
    {
        [SerializeField] Slider slider;

        [SerializeField] AK.Wwise.Event UiMenuSliderTickDown_00_SFX;
        [SerializeField] AK.Wwise.Event UiMenuSliderTickUp_00_SFX;

        string key;
        bool isOptionsDisplayed = false;

        public void Init(string playerPrefKey)
        {
            key = playerPrefKey;
            slider.value = PlayerPrefs.GetFloat(key);
            isOptionsDisplayed = true;
        }

        public void OnOptionsHidden()
        {
            isOptionsDisplayed = false;
        }

        public void OnValueChanged()
        {
            if (!isOptionsDisplayed)
                return;
            UiMenuSliderTickUp_00_SFX.Post(gameObject);
            PlayerPrefs.SetFloat(key, slider.value);


        }

        public void Select()
        {
            slider.Select();
            UiMenuSliderTickDown_00_SFX.Post(gameObject);
         
        }
    }
}