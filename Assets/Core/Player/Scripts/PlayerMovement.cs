using Nano.Entity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] PlayerEntity player;

    public void OnNewMoveInput(Vector2 newMoveInput)
    {
        player.playerData.CurrentInput = newMoveInput;
    }
}
