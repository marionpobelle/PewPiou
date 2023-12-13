using Nano.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

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
        text.transform.DOScale(1.2f, .1f).OnComplete(() =>
        {
            text.text = associatedPlayer.score.ToString();
            text.transform.DOScale(1f, .1f);
        });
    }
}
