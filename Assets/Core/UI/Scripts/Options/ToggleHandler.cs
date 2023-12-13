using UnityEngine;
using UnityEngine.UI;

namespace Nano.UI
{
    public class ToggleHandler : MonoBehaviour
    {
        [SerializeField] Toggle toggle;

        [SerializeField] AK.Wwise.Event UiMenuSelect_00_SFX;
        [SerializeField] AK.Wwise.Event UiMenuBack_00_SFX;

        string key;
        bool isOptionsDisplayed = false;

        public void Init(string playerPrefKey)
        {
            key = playerPrefKey;

            int playerPrefValue = PlayerPrefs.GetInt(key);

            if (playerPrefValue != 0 && playerPrefValue != 1)
            {
                Debug.Log("resetKey because =1 : " + (playerPrefValue == 1) + " & =0 : " + (playerPrefValue == 0));
                PlayerPrefs.SetInt(key, 0);
            }

            toggle.isOn = PlayerPrefs.GetInt(key) == 1;
            isOptionsDisplayed = true;

            toggle.onValueChanged.AddListener(OnValueChanged);
        }

        public void OnOptionsHidden()
        {
            isOptionsDisplayed = false;
        }

        public void OnValueChanged(bool newValue)
        {
            if (!isOptionsDisplayed)
                return;

            Debug.Log((newValue ? 1 : 0));

            PlayerPrefs.SetInt(key, (newValue ? 1 : 0));
    
                
           
        }
    }
}