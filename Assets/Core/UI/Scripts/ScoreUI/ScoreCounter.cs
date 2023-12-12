using Nano.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    PlayerData associatedPlayer;
    int playerNumber;

    public void Init(PlayerData playerData, int thisPlayerNumber)
    {
        associatedPlayer = playerData;
        playerNumber = thisPlayerNumber;
    }

    public void UpdateScore()
    {
        text.text = "Score P" + playerNumber + ": " + associatedPlayer.score.ToString();
    }
}
