using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEasyMovement : EnemyMovement
{
    [SerializeField, Tooltip("How large the pattern is, float")]float movementHorizontalStep = 5.0f;
    [SerializeField, Tooltip("How hight the pattern is, float")] float movementVerticalStep = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        InitializeMovement();
        InitSequence();

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
        movementSequence.Append(transform.DOMove((originScreenPoint + Vector3.left* movementHorizontalStep*2), maxDurationPhase1/3).SetEase(animCurve));
        movementSequence.Append(transform.DOMove((originScreenPoint + Vector3.left* movementHorizontalStep*3 + Vector3.down* movementVerticalStep), maxDurationPhase1/6).SetEase(animCurve));
        movementSequence.Append(transform.DOMove((originScreenPoint + Vector3.left* movementHorizontalStep*4 + Vector3.up* movementVerticalStep), maxDurationPhase1/6).SetEase(animCurve));
        movementSequence.Append(transform.DOMove((originScreenPoint + Vector3.left* movementHorizontalStep*6), maxDurationPhase1/3).SetEase(animCurve));
        movementSequence.AppendCallback(() => SwitchPhase(Phase.Phase2));
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
