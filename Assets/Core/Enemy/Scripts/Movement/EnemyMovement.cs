using DG.Tweening;
using System;
using System.Linq;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UIElements;
using Sirenix.OdinInspector;

public class EnemyMovement : MonoBehaviour
{
    public enum Phase { Phase0, Phase1, Phase2 };

    [BoxGroup("COMPONENTS", ShowLabel = true)]
    public SpriteRenderer sprite;

    [Space]

    protected Phase currentPhase;

    [SerializeField, Tooltip("Duration Phase 0 in seconds")] protected float maxDurationPhase0 = 5.0f;
    [SerializeField, Tooltip("Duration Phase 1 in seconds")] protected float maxDurationPhase1 = 10.0f;
    [SerializeField, Tooltip("Duration Phase 2 in seconds")] protected float maxDurationPhase2 = 5.0f;

    protected Vector3 spawnPoint;
    float screenBorderOffset = 3.0f;
    Vector2 screenBoundX;
    Vector2 screenBoundY;

    [SerializeField, Tooltip("How far the initial position for an enemy is from the right border of the screen, float")] float screenInnerOffset = 4.0f;
    protected Vector3 originScreenPoint;

    protected enum Pattern { Horizontal, Vertical, Circle };
    protected Pattern pattern;

    [SerializeField, Tooltip("Varies the speed of the object in one movement")] protected AnimationCurve animCurve;
    protected Sequence movementSequence;

    public bool isRecruted;

    protected void InitializeMovement()
    {
        screenBoundX = new Vector2(28, 55);
        screenBoundY = new Vector2(-34, 34);
        isRecruted = false;
        //Phase
        currentPhase = Phase.Phase0;
        //Spawn
        float spawnHalfOffset = 1.0f;
        //spawnPoint.x = UnityEngine.Random.Range(screenBoundX[1] + spawnHalfOffset, screenBoundX[1] + screenBorderOffset);
        //spawnPoint.y = UnityEngine.Random.Range(screenBoundY[0], screenBoundY[1]);
        //spawnPoint.z = 0.0f;

        spawnPoint = transform.position;
        //Origin
        originScreenPoint = new Vector3(50, transform.position.y, 0.0f);
        //UnityEngine.Random.Range(screenBoundY[0] + screenInnerOffset, screenBoundY[1] - screenInnerOffset) Y
        //UnityEngine.Random.Range(screenBoundX[1] / 2, screenBoundX[1] - screenInnerOffset) X
        //Movement sequence
        EnemyMovementLoop();
        //transform.position = spawnPoint;
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
