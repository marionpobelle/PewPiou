using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAverageMovement : EnemyMovement
{
    [SerializeField, Tooltip("Radius of the circle")] float rotationRadius = 10.0f;
    [SerializeField, Tooltip("Speed at which the enemy goes in a circle")] float rotationSpeed = 30.0f;

    void Start()
    {
        InitializeMovement();
        InitSequence();
    }

    private void FixedUpdate()
    {
        if(currentPhase == Phase.Phase1)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
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
        movementSequence.AppendCallback(() => {
            sprite.transform.localScale = new Vector3(-sprite.transform.localScale.x, sprite.transform.localScale.y, sprite.transform.localScale.z);
        });
        movementSequence.Append(transform.DOMove(spawnPoint, maxDurationPhase2));
        movementSequence.AppendCallback(() => Destroy(gameObject));
    }

    private void SwitchPhase(Phase phase)
    {
        currentPhase = phase;
    }
}
