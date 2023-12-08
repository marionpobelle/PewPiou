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
        //bool deletingShield; //used because deleting shields takes time because of tween, to avoid repetition


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
                    _newShield.GetComponent<MeshRenderer>().material.color = new Color(1, 0, 0, .5f);
                    break;
                case Data.BulletType.Blue:
                    _newShield.GetComponent<MeshRenderer>().material.color = new Color(0, 0, 1, .5f);
                    break;
                case Data.BulletType.Green:
                    _newShield.GetComponent<MeshRenderer>().material.color = new Color(0, 1, 0, .5f);
                    break;
            }
        }

        public void RemoveShield(Shield _shield)
        {
            shieldList.Remove(_shield);
            player.playerData.shieldTypeList.Remove(player.playerData.shieldTypeList[0]);
            _shield.transform.DOScale(_shield.fixedScale - 0.1f, .3f).OnComplete(() =>
            {
                _shield.transform.DOScale(_shield.fixedScale + 0.3f, .2f).OnComplete(() =>
                {
                    _shield.DOKill();
                    Destroy(_shield.gameObject);
                    shieldCollider.radius = .5f;
                });
            });
        }

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
                }
                else //WRONG SHIELD
                {
                    Debug.Log("Wrong shield");
                }
            }
        }
    }

}