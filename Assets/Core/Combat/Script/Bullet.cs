using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nano.Data;
using System;
using DG.Tweening;
using Sirenix.OdinInspector;

namespace Nano.Combat
{
    public class Bullet : MonoBehaviour
    {
        [BoxGroup("COMPONENTS", ShowLabel = true)]
        [SerializeField, Tooltip("pas touche � celui l�")] Rigidbody rb;
        [BoxGroup("COMPONENTS", ShowLabel = true)]
        [SerializeField] MeshRenderer bulletRenderer;
        [BoxGroup("COMPONENTS", ShowLabel = true)]
        [SerializeField] SpriteRenderer spriteRenderer;
        [Space]
        float bulletSpeed = 10;
        public BulletType bulletType;
        Vector3 bulletDir;
        GameObject parentEnemy;
        bool backToSender = false;
        public bool convertingBullet = false;
        [SerializeField, Tooltip("How many seconds the bullet waits before getting destroyed automatically, float")] float destroyAfterTime = 15.0f;
        [SerializeField] List<Sprite> spriteList = new List<Sprite>();
        [SerializeField] GameObject hitEffect;

        public void Init(Vector3 dir, float speed)
        {
            bulletDir = dir;
            bulletSpeed = speed;
            bulletType = (BulletType)UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(BulletType)).Length);
            spriteRenderer.sprite = spriteList[(int)bulletType];
            switch (bulletType)
            {
                case BulletType.Red:
                    bulletRenderer.material.SetColor("_Color", Color.red);
                    break;
                case BulletType.Blue:
                    bulletRenderer.material.SetColor("_Color", Color.blue);
                    break;
                case BulletType.Green:
                    bulletRenderer.material.SetColor("_Color", Color.green);
                    break;
            }
            Destroy(gameObject, destroyAfterTime);
        }

        public void SetParentEnemy(GameObject enemy)
        {
            parentEnemy = enemy;
        }

        public GameObject GetParentEnemy()
        {
            return parentEnemy;
        }

        private void FixedUpdate()
        {
            if (backToSender) return;
            rb.velocity = bulletDir * bulletSpeed;   
        }

        public void BackToSender()
        {
            backToSender = true;
            rb.velocity = Vector3.zero;
            if (parentEnemy == null) return;
            transform.DORotate(new Vector3(0, 0, 180), .2f);
            DOVirtual.DelayedCall(.35f, () => Instantiate(hitEffect, gameObject.transform.position, Quaternion.identity));
            transform.DOMove(parentEnemy.transform.position, .4f).OnComplete(() =>
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
