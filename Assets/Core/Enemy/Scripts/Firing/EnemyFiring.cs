using DG.Tweening;
using Nano.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFiring : MonoBehaviour
{
    public enum Phase { Phase0, Phase1, Phase2 };

    protected Transform firePoint;
    [SerializeField] protected Bullet bulletPrefab;
    public List<Bullet> firedBulletSequence;
    public List<Transform> playerTransforms;


    protected Phase currentPhase;
    protected bool isFiringSequencePlaying = false;
    protected Sequence firingSequence;

    [SerializeField, Tooltip("Time before firing starts")][Range(0.0f, 10.0f)] protected float fireDelay = 2.0f;
    [SerializeField, Tooltip("Duration between fired bullets. float")] protected float fireRate = 2.0f;

    protected void InitFiring()
    {
        firePoint = this.gameObject.transform.GetChild(0);
        //remplir playerTransforms
        currentPhase = (Phase)gameObject.GetComponent<EnemyMovement>().GetPhase();
    }
    protected void Fire(bool _convertingBullet = false)
    {
        Bullet bullet = Instantiate(bulletPrefab, firePoint.position + Vector3.left, Quaternion.identity);
        firedBulletSequence.Add(bullet);
        bullet.SetParentEnemy(gameObject);
        bullet.convertingBullet = _convertingBullet;
        //int randomPlayer = Random.Range(0, 2);
        //Vector3 directionTowardsPlayer;
        //if (randomPlayer == 0) directionTowardsPlayer = ComputePlayerDirection(playerTransforms[0]);
        //else directionTowardsPlayer = ComputePlayerDirection(playerTransforms[1]);
        //Changer Vector3.left pour direction du player quand ce sera bon
        bullet.Init(Vector3.left);
    }

    protected Vector3 ComputePlayerDirection(Transform player)
    {
        return (this.transform.position - player.position).normalized;
    }

    protected virtual void InitSequence()
    {
        Debug.Log("Enemy Sequence");
    }
}
