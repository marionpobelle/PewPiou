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
        int deviceId;

        private void Awake()
        {
            joinPlayerInput.Enable();

            joinPlayerInput.performed += JoinPlayer;
            GameManager.onGameStart += UnsubscribeEvent;
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
            Debug.Log("Start");
            if (obj.control.device.deviceId == deviceId)
            {
                Debug.Log(deviceId);
                return;
            }

            var joinedPlayer = manager.JoinPlayer(spawnedPlayers, -1, "Player", obj.control.device);
            spawnedPlayers++;

            deviceId = obj.control.device.deviceId;
            OnPlayerAdded?.Invoke(joinedPlayer.GetComponent<PlayerEntity>());
        }
    }
}