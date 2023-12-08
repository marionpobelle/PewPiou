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

        public void OnButtonNorth(InputValue value)
        {
#if UNITY_EDITOR
            if (value.Get<float>() > .5f)
            {
                GetComponent<SquadronManager>().TestAddSquadronMember();
            }
#endif
        }
    }
}