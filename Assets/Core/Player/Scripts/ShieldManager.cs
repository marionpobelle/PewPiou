using Nano.Entity;
using Nano.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;

namespace Nano.Player
{
    public class ShieldManager : MonoBehaviour
    {
        [SerializeField] PlayerEntity player;
        [SerializeField] SphereCollider shieldCollider;
        [SerializeField] Shield shieldPrefab;
        [SerializeField] float shieldSizeAugmentation;
        [SerializeField] float shieldResetTime;
        [SerializeField] float maxShieldSize;
        float shieldTimer;
        [SerializeField] int maxNumberShield;
        List<Shield> shieldList = new List<Shield>();
        [SerializeField] List<Texture> inputSpriteList = new List<Texture>();

        [Button("ADD SHIELD")]
        public void AddShield(Data.BulletType _shieldType = Data.BulletType.Blue)
        {
            if (shieldList.Count >= maxNumberShield) return;
            Shield _newShield = Instantiate(shieldPrefab, transform);
            _newShield.shieldType = _shieldType;
            _newShield.fixedScale = maxShieldSize - shieldSizeAugmentation * shieldList.Count;
            _newShield.transform.localScale = Vector3.zero;
            shieldList.Add(_newShield);
            player.playerData.shieldTypeList.Add(_newShield.shieldType);
            for (int i = 0; i < shieldList.Count; i++)
            {
                Shield _shield = shieldList[i];
                _shield.transform.DOScale(_shield.fixedScale + 0.3f, .3f).OnComplete(() =>
                {
                    _shield.transform.DOScale(_shield.fixedScale, .1f);
                });
            }
            if (shieldList.Count > 0) shieldCollider.radius = maxShieldSize / 2;
            shieldTimer = shieldResetTime;
            switch (_shieldType)
            {
                case Data.BulletType.Red:
                    _newShield.shieldRenderer.material.SetColor("_Color", Color.red);
                    _newShield.shieldRenderer.material.SetTexture("_button_label_texture", inputSpriteList[0]);
                    break;
                case Data.BulletType.Blue:
                    _newShield.shieldRenderer.material.SetColor("_Color", Color.blue);
                    _newShield.shieldRenderer.material.SetTexture("_button_label_texture", inputSpriteList[1]);
                    break;
                case Data.BulletType.Green:
                    _newShield.shieldRenderer.material.SetColor("_Color", Color.green);
                    _newShield.shieldRenderer.material.SetTexture("_button_label_texture", inputSpriteList[2]);
                    break;
            }
            _newShield.shieldRenderer.material.SetFloat("_wiggle_seed", Random.Range(0.0f, 10.0f));
            _newShield.shieldRenderer.material.SetFloat("_bubble_angle", shieldList.Count - .5f);
        }

        public void RemoveShield(Shield _shield)
        {
            shieldList.Remove(_shield);
            player.playerData.shieldTypeList.Remove(player.playerData.shieldTypeList[0]);
            _shield.shieldRenderer.material.DOFloat(0.19f, "_wiggle_size", .3f);
            _shield.shieldRenderer.material.DOFloat(0f, "_circle_stroke_width", .3f);
            _shield.transform.DOScale(_shield.fixedScale - 2f, .3f).OnComplete(() =>
            {
                _shield.shieldRenderer.material.DOColor(new Color(0,0,0,0), "_Color", .2f);
                _shield.transform.DOScale(_shield.fixedScale + 5f, .2f).OnComplete(() =>
                {
                    _shield.DOKill();
                    Destroy(_shield.gameObject);
                    shieldCollider.radius = .5f;
                });
            });
            for (int i = 0; i < shieldList.Count; i++)
            {
                Shield _sizeFixShield = shieldList[i];
                _sizeFixShield.fixedScale = maxShieldSize - shieldSizeAugmentation * i;
                _sizeFixShield.transform.DOScale(_sizeFixShield.fixedScale + 0.3f, .3f).OnComplete(() =>
                {
                    _sizeFixShield.transform.DOScale(_sizeFixShield.fixedScale, .1f);
                });
                _sizeFixShield.shieldRenderer.material.DOFloat(i - .5f, "_bubble_angle", .2f);
            }
        }

        [Button("REMOVE ALL SHIELDS")]
        public void RemoveAllShields()
        {
            //if (shieldList.Count == 0) return;
            for (int i = 0; i < shieldList.Count; i++)
            {
                Shield _shield = shieldList[i];
                RemoveShield(_shield);
            }
        }

        private void Update()
        {
            shieldTimer -= Time.deltaTime;
            if (shieldTimer <= 0 && shieldList.Count > 0)
            {
                RemoveAllShields();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            Bullet _bullet = other.GetComponent<Bullet>();
            //Here detect bullet type
            if (_bullet != null)
            {
                if (shieldList.Count == 0) //NO SHIELD
                {
                    //DAMAGE ?
                } else if (_bullet.bulletType == shieldList[0].shieldType) //GOOD SHIELD
                {
                    Debug.Log("Good shield");
                    RemoveShield(shieldList[0]);

                    if (_bullet.convertingBullet)
                    {
                        _bullet.BackToSender();
                        DOVirtual.DelayedCall(.5f, () => player.squadronManager.AddFollower());
                    } else
                    {
                        //SHOW JUICY SCORE
                        _bullet.ExplodeBullet();
                        gameObject.GetComponent<PlayerScore>().IncreaseScoreHitNote();
                    }

                }
                else //WRONG SHIELD
                {
                    Debug.Log("Wrong shield");
                    RemoveShield(shieldList[0]);
                    Destroy(other.gameObject);
                }
            }
        }
    }

}