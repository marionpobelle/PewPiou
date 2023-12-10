using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFiring : MonoBehaviour
{
    public enum Phase { Phase0, Phase1, Phase2 };

    protected Transform firePoint;
    [SerializeField] protected GameObject bulletPrefab;
    public List<GameObject> firedBulletSequence;

    [SerializeField, Tooltip("How fast the bullet goes, float")] protected float speed = 100.0f;

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
    protected void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        firedBulletSequence.Add(bullet);

        int randomPlayer = Random.Range(0, 2);
        Vector3 directionTowardsPlayer;
        if (randomPlayer == 0) directionTowardsPlayer = ComputePlayerDirection(playerTransforms[0]);
        else directionTowardsPlayer = ComputePlayerDirection(playerTransforms[1]);

        Rigidbody rigidbodyBullet = bullet.GetComponent<Rigidbody>();
        //Changer Vector3.left pour direction du player quand se sera bon
        rigidbodyBullet.AddForce(Vector3.left * speed, ForceMode.Impulse);
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
