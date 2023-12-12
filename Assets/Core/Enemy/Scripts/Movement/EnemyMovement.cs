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
    Vector2 screenBoundX;
    Vector2 screenBoundY;

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
        spawnPoint = transform.position;
        //Origin
        originScreenPoint = new Vector3(50, transform.position.y, 0.0f);
        //Mouvement
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
