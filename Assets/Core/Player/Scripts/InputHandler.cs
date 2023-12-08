using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Nano.Player
{
    public class InputHandler : MonoBehaviour
    {
        [SerializeField] PlayerMovement playerMovement;
        [SerializeField] ShieldManager shieldManager;

        public void OnMove(InputValue value)
        {
            playerMovement.OnNewMoveInput(value.Get<Vector2>());
        }

        public void OnButtonEast(InputValue value)
        {
            Debug.Log("BUTTON");
            shieldManager.AddShield(Data.BulletType.Red);
        }

        public void OnButtonWest(InputValue value)
        {
            Debug.Log("BUTTON");
            shieldManager.AddShield(Data.BulletType.Blue);
        }

        public void OnButtonNorth(InputValue value)
        {
            Debug.Log("BUTTON");
            shieldManager.AddShield(Data.BulletType.Green);
        }
    }
}