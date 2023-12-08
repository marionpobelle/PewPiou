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

        [Header("Movement Settings")]
        [SerializeField] float moveSpeed = 60;

        private void Update()
        {
            //set rb velocity to move value. The reason we are using rigidbody is to allow players to push each others
            rb.velocity = new Vector3(player.playerData.CurrentInput.x, player.playerData.CurrentInput.y, 0) * moveSpeed;

            //Dirty Safety measure : we make sure the player is at 0 in z position
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }

        public void OnNewMoveInput(Vector2 newMoveInput)
        {
            player.playerData.CurrentInput = newMoveInput;
        }
    }
}