using System;
using UnityEngine;
using UnityEngine.UI;

namespace Nano.UI
{
    public class SliderHandler : MonoBehaviour
    {
        [SerializeField] Slider slider;

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

            PlayerPrefs.SetFloat(key, slider.value);
        }

        public void Select()
        {
            slider.Select();
        }
    }
}