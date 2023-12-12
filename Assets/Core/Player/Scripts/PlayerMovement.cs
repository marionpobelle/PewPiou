using Nano.Entity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nano.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] PlayerEntity player;
        [SerializeField] Rigidbody rb;
        [SerializeField] float screenHeigt = 33.3f;
        [SerializeField] float screenLength = 55f;

        [Header("Movement Settings")]
        [SerializeField] float moveSpeed = 60;

        private void Update()
        {
            //set rb velocity to move value. The reason we are using rigidbody is to allow players to push each others
            rb.velocity = new Vector3(player.playerData.CurrentInput.x, player.playerData.CurrentInput.y, 0) * moveSpeed;

            //clamp the player position
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, -screenLength, screenLength),
                Mathf.Clamp(transform.position.y, -screenHeigt, screenHeigt), 
                0);
        }

        public void OnNewMoveInput(Vector2 newMoveInput)
        {
            player.playerData.CurrentInput = newMoveInput;
        }
    }
}