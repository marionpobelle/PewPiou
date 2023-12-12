using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nano.Data;
using System;
using DG.Tweening;

namespace Nano.Combat
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField, Tooltip("pas touche à celui là")] Rigidbody rb;
        [SerializeField, Tooltip("How fast the bullet goes, float")] float speed;
        public BulletType bulletType;
        [SerializeField] MeshRenderer bulletRenderer;
        Vector3 bulletDir;
        GameObject parentEnemy;
        bool backToSender = false;
        public bool convertingBullet = false;
        [SerializeField, Tooltip("How many seconds the bullet waits before getting destroyed automatically, float")] float destroyAfterTime = 15.0f;

        [SerializeField] AK.Wwise.Event EnemyNote1_00_SFX;
        [SerializeField] AK.Wwise.Event EnemyNote2_00_SFX;
        [SerializeField] AK.Wwise.Event EnemyNote3_00_SFX;

        public void Init(Vector3 dir)
        {
            bulletDir = dir;
            bulletType = (BulletType)UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(BulletType)).Length);
            switch (bulletType)
            {
                case BulletType.Red:
                    bulletRenderer.material.SetColor("_Color", Color.red);
                    EnemyNote1_00_SFX.Post(gameObject);
                    break;
                case BulletType.Blue:
                    bulletRenderer.material.SetColor("_Color", Color.blue);
                    EnemyNote2_00_SFX.Post(gameObject);
                    break;
                case BulletType.Green:
                    bulletRenderer.material.SetColor("_Color", Color.green);
                    EnemyNote3_00_SFX.Post(gameObject);
                    break;
            }
            Destroy(gameObject, destroyAfterTime);
        }

        public void SetParentEnemy(GameObject enemy)
        {
            parentEnemy = enemy;
        }

        private void FixedUpdate()
        {
            if (backToSender) return;
            rb.velocity = bulletDir * speed;   
        }

        public void BackToSender()
        {
            backToSender = true;
            rb.velocity = Vector3.zero;
            if (parentEnemy == null) return;
            transform.DORotate(new Vector3(0, 0, 180), .2f);
            transform.DOMove(parentEnemy.transform.position, .5f).OnComplete(() =>
            {
                Destroy(parentEnemy.gameObject);
                Destroy(this.gameObject);
            });
        }

        public void ExplodeBullet()
        {
            Destroy(this.gameObject);
        }

    }
}
