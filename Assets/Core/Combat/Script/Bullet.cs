using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nano.Data;
using System;

namespace Nano.Combat
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField, Tooltip("pas touche à celui là")] Rigidbody rb;
        [SerializeField, Tooltip("How fast the bullet goes, float")] float speed;
        public BulletType bulletType;
        Vector3 bulletDir;
        GameObject parentEnemy;

        public void Init(Vector3 dir)
        {
            bulletDir = dir;
            bulletType = (BulletType)UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(BulletType)).Length);
        }

        public void SetParentEnemy(GameObject enemy)
        {
            parentEnemy = enemy;
        }

        void OnBecameInvisible()
        {
            Destroy(gameObject);
        }

        private void FixedUpdate()
        {
            rb.velocity = bulletDir * speed;   
        }

    }
}
