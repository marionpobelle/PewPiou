using DG.Tweening;
using System;
using System.Linq;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyMovement : MonoBehaviour
{
    public enum Phase { Phase0, Phase1, Phase2 };
    protected Phase currentPhase;

    [SerializeField, Tooltip("Duration Phase 0 in seconds")] protected float maxDurationPhase0 = 5.0f;
    [SerializeField, Tooltip("Duration Phase 1 in seconds")] protected float maxDurationPhase1 = 10.0f;
    [SerializeField, Tooltip("Duration Phase 2 in seconds")] protected float maxDurationPhase2 = 5.0f;

    protected Vector3 spawnPoint;
    float screenBorderOffset = 2.0f;
    Vector2 screenBoundX;
    Vector2 screenBoundY;

    float screenInnerOffset = 2.0f;
    protected Vector3 originScreenPoint;

    protected enum Pattern { Horizontal, Vertical, Circle };
    protected Pattern pattern;

    [SerializeField, Tooltip("Varies the speed of the object in one movement")] protected AnimationCurve animCurve;
    protected Sequence movementSequence;

    public bool isRecruted;

    protected void InitializeMovement()
    {
        screenBoundX = new Vector2(0, 55);
        screenBoundY = new Vector2(-34, 34);
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

    public Phase GetPhase()
    {
        return currentPhase;
    }
}
