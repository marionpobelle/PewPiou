using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nano.Data;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;

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
        [BoxGroup("COMPONENTS", ShowLabel = true)]
        [SerializeField] ParticleSystem specialParticleSystem;
        [Space]
        float bulletSpeed = 10;
        public BulletType bulletType;
        Vector3 bulletDir;
        GameObject parentEnemy;
        bool backToSender = false;
        public bool convertingBullet = false;
        public bool hasCollided = false;
        [SerializeField, Tooltip("How many seconds the bullet waits before getting destroyed automatically, float")] float destroyAfterTime = 15.0f;
        [SerializeField] List<Sprite> spriteList = new List<Sprite>();
        [SerializeField] GameObject hitEffect;

        [SerializeField] AK.Wwise.Event EnemyNote1_00_SFX;
        [SerializeField] AK.Wwise.Event EnemyNote2_00_SFX;
        [SerializeField] AK.Wwise.Event EnemyNote3_00_SFX;
        [SerializeField] AK.Wwise.Event SpecialBullet_00_SFX;
        [SerializeField] GameObject floatingPoints;

        public void Init(Vector3 dir, float speed)
        {
            bulletDir = dir;
            bulletSpeed = speed;
            bulletType = (BulletType)UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(BulletType)).Length);
            spriteRenderer.sprite = spriteList[(int)bulletType];
            float _seed = Random.Range(0.0f, 10.0f);
            bulletRenderer.material.SetFloat("_seed", _seed);
            switch (bulletType)
            {
                case BulletType.Red:
                    bulletRenderer.material.SetColor("_Color", new Color32(255, 0, 30, 0));
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
            if (convertingBullet)
            {

                //MagicBullet.Post
                SpecialBullet_00_SFX.Post(gameObject);
                bulletRenderer.material.SetFloat("_lastbullet", 1.0f);
                specialParticleSystem.gameObject.SetActive(true);
            } else
            {
                bulletRenderer.material.SetFloat("_lastbullet", 0f);
                specialParticleSystem.gameObject.SetActive(false);
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

        public void BackToSender(string textHit)
        {
            backToSender = true;
            rb.velocity = Vector3.zero;
            if (parentEnemy == null) return;
            spriteRenderer.transform.DORotate(new Vector3(0, 0, 180), .2f);
            transform.DOMove(parentEnemy.transform.position, .4f).OnComplete(() =>
            {
                Destroy(parentEnemy.gameObject);
                ExplodeBullet(textHit);
            });
        }

        public void ExplodeBullet(string textHit = " ")
        {
            Instantiate(hitEffect, gameObject.transform.position, Quaternion.identity);
            GameObject points = Instantiate(floatingPoints, parentEnemy.transform.position, Quaternion.identity) as GameObject;
            TextMeshPro pointText = points.transform.GetChild(0).GetComponent<TextMeshPro>();

           pointText.SetText(textHit);
            transform.DOScale(1.7f, .2f).OnComplete(() =>
            {
                GetComponent<Collider>().enabled = false;
                transform.DOScale(.7f, .1f).OnComplete(() =>
                {
                    Destroy(this.gameObject);
                });
            });
        }
    }
}
