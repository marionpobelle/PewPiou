using DG.Tweening;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyMovement : MonoBehaviour
{
    protected enum Phase { Phase0, Phase1, Phase2 };
    protected Phase currentPhase;

    [SerializeField] protected float maxDurationPhase0 = 5.0f;
    [SerializeField] protected float maxDurationPhase1 = 10.0f;
    [SerializeField] protected float maxDurationPhase2 = 5.0f;

    protected Vector3 spawnPoint;
    [SerializeField] float screenBorderOffset = 2.0f;
    [SerializeField] Vector2 screenBoundX;
    [SerializeField] Vector2 screenBoundY;

    [SerializeField] float screenInnerOffset = 2.0f;
    protected Vector3 originScreenPoint;

    protected enum Pattern { Horizontal, Vertical, Circle };
    protected Pattern pattern;

    [SerializeField] protected AnimationCurve animCurve;
    protected Sequence movementSequence;

    public bool isRecruted;

    protected void InitializeMovement()
    {
        isRecruted = false;
        //Phase
        currentPhase = Phase.Phase0;
        //Spawn
        bool isEnemyOutOfScreen = false;
        float spawnHalfOffset = 1.0f;
        while (isEnemyOutOfScreen == false)
        {
            Vector2 randomSpawn = new Vector2(UnityEngine.Random.Range(screenBoundX[0] - screenBorderOffset, screenBoundX[1] + screenBorderOffset),
                                         UnityEngine.Random.Range(screenBoundY[0] - screenBorderOffset, screenBoundX[0] + screenBorderOffset));
            if (!((randomSpawn[0] > screenBoundX[0] - spawnHalfOffset) &&
                (randomSpawn[0] < screenBoundX[1] + spawnHalfOffset) &&
                (randomSpawn[1] > screenBoundY[0] - spawnHalfOffset) &&
                (randomSpawn[1] < screenBoundY[1] + spawnHalfOffset)))
            {
                spawnPoint.x = randomSpawn.x;
                spawnPoint.y = randomSpawn.y;
                spawnPoint.z = 0.0f;
                isEnemyOutOfScreen = true;
            }
        }
        //Origin
        originScreenPoint = new Vector3(UnityEngine.Random.Range(screenBoundX[0] + screenInnerOffset, screenBoundX[1] - screenInnerOffset),
                                         UnityEngine.Random.Range(screenBoundY[0] + screenInnerOffset, screenBoundX[0] - screenInnerOffset), 0.0f);
        //Movement sequence
        EnemyMovementLoop();
    }

    protected virtual void EnemyMovementLoop()
    {
        Debug.Log("Enemy moving");
    }

    protected virtual void InitSequence()
    {
        Debug.Log("Enemy Sequence");
    }
}
