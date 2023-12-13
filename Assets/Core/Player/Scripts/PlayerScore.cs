using Nano.Player;
using Nano.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    [SerializeField] PlayerEntity player;
    [SerializeField, Tooltip("by how much the score increases when you get a bird")] float scoreIncrAddBird = 300.0f;
    [SerializeField, Tooltip("by how much the score decreases when you lose a bird")] float scoreDecrRemoveBird = -300.0f;
    [SerializeField, Tooltip("by how much the score increases when you hit a note")] float scoreIncrHitNote = 100.0f;
    [SerializeField, Tooltip("combo amount, 2 bullets")] float scoreCombo2Bullets = 50.0f;
    [SerializeField, Tooltip("combo amount, 3 bullets")] float scoreCombo3Bullets = 200.0f;

    public void IncreaseScoreAddBird(bool combo3bullets = false)
    {
        if (combo3bullets) OnChangeScore(scoreIncrAddBird + scoreCombo3Bullets);
        else OnChangeScore(scoreIncrAddBird);
    }

    public void IncreaseScoreHitNote(bool combo2bullets = false)
    {
        if (combo2bullets) OnChangeScore(scoreIncrHitNote + scoreCombo2Bullets);
        else OnChangeScore(scoreIncrHitNote);
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
