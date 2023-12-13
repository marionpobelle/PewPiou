using Nano.Player;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Nano.Managers
{
    public class PlayerJoinManager : MonoBehaviour
    {
        [SerializeField] PlayerInputManager manager;
        [SerializeField] InputAction joinPlayerInput;
        public static event Action<PlayerEntity> OnPlayerAdded;
        int spawnedPlayers = 0;
        int connectedDeviceId = -1;
        float startJoinTime;

        [SerializeField] AK.Wwise.Event UiP1Connected_00_SFX;
        [SerializeField] AK.Wwise.Event UiP2Connected_00_SFX;
        [SerializeField] AK.Wwise.Event BGM_Title;

        private void Awake()
        {
            joinPlayerInput.Enable();

            joinPlayerInput.performed += JoinPlayer;
            GameManager.onGameStart += UnsubscribeEvent;
            startJoinTime = Time.time + .1f;
        }

        private void OnDestroy()
        {
            UnsubscribeEvent();
        }

        private void UnsubscribeEvent()
        {
            joinPlayerInput.performed -= JoinPlayer;
            GameManager.onGameStart -= UnsubscribeEvent;
        }

        private void JoinPlayer(InputAction.CallbackContext obj)
        {
            if (Time.time < startJoinTime)
                return;

            Debug.Log($"{this.GetType()} >> Pressed Join Input");
            if (obj.control.device.deviceId == connectedDeviceId)
            {
                Debug.Log($"{this.GetType()} >> Already connected to device with id {connectedDeviceId}");
                return;
            }

            var joinedPlayer = manager.JoinPlayer(spawnedPlayers, -1, "Player", obj.control.device);
            spawnedPlayers++;
            connectedDeviceId = obj.control.device.deviceId;
            Debug.Log($"{this.GetType()} >> Spawend player {spawnedPlayers} using device with id {connectedDeviceId}");

            OnPlayerAdded?.Invoke(joinedPlayer.GetComponent<PlayerEntity>());
            if (obj.control.device is Gamepad)
            {
                Debug.Log("DEVICE CLASS : " + obj.control.device);
                joinedPlayer.GetComponent<PlayerEntity>().InputHandler.gamepad = Gamepad.all[Gamepad.all.Count - 1];

            }
            
            (spawnedPlayers == 1 ? UiP1Connected_00_SFX : UiP2Connected_00_SFX).Post(gameObject);
        }
    }
}