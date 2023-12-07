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

        private void FixedUpdate()
        {
            rb.velocity = new Vector3(player.playerData.CurrentInput.x, player.playerData.CurrentInput.y, 0) * moveSpeed;
        }

        public void OnNewMoveInput(Vector2 newMoveInput)
        {
            player.playerData.CurrentInput = newMoveInput;
        }
    }
}