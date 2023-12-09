using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAverageMovement : EnemyMovement
{
    [SerializeField, Tooltip("How large the pattern is, float")] float movementHorizontalStep = 25.0f;
    [SerializeField, Tooltip("How hight the pattern is, float")] float movementVerticalStep = 5.0f;

    [SerializeField, Tooltip("Radius of the circle")] float rotationRadius = 20.0f;
    [SerializeField, Tooltip("Speed at which the enemy goes in a circle")] float rotationSpeed = 30.0f;

    // Start is called before the first frame update
    void Start()
    {
        InitializeMovement();
        InitSequence();

    }

    private void Update()
    {
        if(currentPhase == Phase.Phase1)
        {
            transform.RotateAround(new Vector3(originScreenPoint.x - rotationRadius, originScreenPoint.y, 0), Vector3.forward, rotationSpeed * Time.deltaTime);
        }
    }

    protected override void EnemyMovementLoop()
    {
        movementSequence.Play();
    }

    protected override void InitSequence()
    {
        movementSequence = DOTween.Sequence();
        //Phase 0
        movementSequence.Append(transform.DOMove(originScreenPoint, maxDurationPhase0));
        movementSequence.AppendCallback(() => SwitchPhase(Phase.Phase1));
        //Phase 1
        movementSequence.AppendInterval(maxDurationPhase1);
        //Phase 2
        movementSequence.Append(transform.DOMove(spawnPoint, maxDurationPhase2));
        movementSequence.AppendCallback(() => Destroy(gameObject));

    }

    private void SwitchPhase(Phase phase)
    {
        currentPhase = phase;
    }
}
