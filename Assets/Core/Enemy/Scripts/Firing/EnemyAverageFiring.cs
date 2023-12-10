using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAverageFiring : EnemyFiring
{
    [SerializeField, Tooltip("Short duration between fired bullets, float")] protected float shortfireRate = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        InitFiring();
        InitSequence();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentPhase = (Phase)gameObject.GetComponent<EnemyMovement>().GetPhase();
        if (currentPhase == Phase.Phase1 && isFiringSequencePlaying == false)
        {
            isFiringSequencePlaying = true;
            firingSequence.Play();
        }
    }

    protected override void InitSequence()
    {
        firingSequence = DOTween.Sequence();
        firingSequence.AppendInterval(fireDelay);
        firingSequence.AppendCallback(() => Fire());
        firingSequence.AppendInterval(fireRate);
        firingSequence.AppendCallback(() => Fire());
        firingSequence.AppendInterval(shortfireRate);
        firingSequence.AppendCallback(() => Fire());
    }

}
