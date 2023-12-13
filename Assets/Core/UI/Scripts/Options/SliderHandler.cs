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
        public string wwiseParameter;

        string key;
        bool isOptionsDisplayed = false;
        float previousvalue;

        public void Init(string playerPrefKey)
        {
            key = playerPrefKey;
            slider.value = PlayerPrefs.GetFloat(key);
            isOptionsDisplayed = true;
            previousvalue = slider.value;
        }

        public void OnOptionsHidden()
        {
            isOptionsDisplayed = false;
        }

        public void OnValueChanged()
        {
            if (!isOptionsDisplayed)
                return;

            AkSoundEngine.SetRTPCValue(wwiseParameter, slider.value);

            (slider.value > previousvalue ?UiMenuSliderTickUp_00_SFX : UiMenuSliderTickDown_00_SFX).Post(gameObject);
            
            PlayerPrefs.SetFloat(key, slider.value);

            previousvalue = slider.value;
        }

        public void Select()
        {
            slider.Select();
            UiMenuSliderTickDown_00_SFX.Post(gameObject);
         
        }
    }
}