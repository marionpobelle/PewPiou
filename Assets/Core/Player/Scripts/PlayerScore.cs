using Nano.Player;
using Nano.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    [SerializeField] PlayerEntity player;
    [SerializeField, Tooltip("by how much the score increases when you get a bird")] float scoreIncrAddBird = 300.0f;
    [SerializeField, Tooltip("by how much the score decreases when you lose a bird")] float scoreDecrRemoveBird = 300.0f;
    [SerializeField, Tooltip("by how much the score increases when you hit a note")] float scoreIncrHitNote = 100.0f;

    public void IncreaseScoreAddBird()
    {
        OnChangeScore(scoreIncrAddBird);
    }

    public void IncreaseScoreHitNote()
    {
        OnChangeScore(scoreIncrHitNote);
    }

    public void DecreaseScoreRemoveBird()
    {
        OnChangeScore(scoreDecrRemoveBird);
    }

    void OnChangeScore(float scoreChange)
    {
        player.playerData.score += scoreChange;

        ScoreUI.Instance.UpdateScore(player.playerData);
    }
}
