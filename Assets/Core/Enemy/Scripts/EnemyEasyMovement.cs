using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEasyMovement : EnemyMovement
{
    // Start is called before the first frame update
    void Start()
    {
        InitializeMovement();
        InitSequence();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        movementSequence.Append(transform.DOMove((originScreenPoint + Vector3.up*50), maxDurationPhase1/2).SetEase(animCurve));
        movementSequence.Append(transform.DOMove(originScreenPoint, maxDurationPhase1/2).SetEase(animCurve));
        movementSequence.AppendCallback(() => SwitchPhase(Phase.Phase2));
        //Phase 2
        movementSequence.Append(transform.DOMove(spawnPoint, maxDurationPhase2));
        movementSequence.AppendCallback(() => Destroy(gameObject));

    }

    private void SwitchPhase(Phase phase)
    {
        currentPhase = phase;
    }
}
