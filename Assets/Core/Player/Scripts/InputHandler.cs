using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Nano.Player
{
    public class InputHandler : MonoBehaviour
    {
        [SerializeField] PlayerMovement playerMovement;

        public void OnMove(InputValue value)
        {
            playerMovement.OnNewMoveInput(value.Get<Vector2>());
        }
    }
}