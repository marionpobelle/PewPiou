using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Nano.Player
{
    public class InputHandler : MonoBehaviour
    {
        [SerializeField] PlayerMovement playerMovement;
        [SerializeField] ShieldManager shieldManager;
        public Gamepad gamepad;

        public static event Action onPausePressed;

        private bool isInputsFrozen = false;

        public void OnMove(InputValue value)
        {
            playerMovement.OnNewMoveInput(value.Get<Vector2>());
        }

        public void OnButtonEast(InputValue value)
        {
            if (isInputsFrozen)
                return;
            shieldManager.AddShield(Data.BulletType.Red);
        }

        public void OnButtonWest(InputValue value)
        {
            if (isInputsFrozen)
                return;
            shieldManager.AddShield(Data.BulletType.Blue);
        }

        public void OnButtonSouth(InputValue value)
        {
            if (isInputsFrozen)
                return;
            shieldManager.AddShield(Data.BulletType.Green);
#if UNITY_EDITOR
            //if (value.Get<float>() > .5f)
            //{
            //    GetComponent<SquadronManager>().TestAddSquadronMember();
            //}
#endif
        }

        public void OnPause(InputValue value)
        {
            if (value.Get<float>() > .5f)
                onPausePressed?.Invoke();
        }

        public void FreezeInputs()
        {
            isInputsFrozen = true;
        }

        public void UnfreezeInputs()
        {
            isInputsFrozen = false;
        }
    }
}