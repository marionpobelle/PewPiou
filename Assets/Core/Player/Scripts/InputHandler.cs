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
            shieldManager.AddShield(Data.BulletType.Red);
        }

        public void OnButtonWest(InputValue value)
        {
            shieldManager.AddShield(Data.BulletType.Blue);
        }

        public void OnButtonSouth(InputValue value)
        {
            shieldManager.AddShield(Data.BulletType.Green);
#if UNITY_EDITOR
            //if (value.Get<float>() > .5f)
            //{
            //    GetComponent<SquadronManager>().TestAddSquadronMember();
            //}
#endif
        }
    }
}