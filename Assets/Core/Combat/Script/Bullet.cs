using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nano.Data;
using System;

namespace Nano.Combat
{
    public class Bullet : MonoBehaviour
    {
        public BulletType bulletType;

        private void Start()
        {
            bulletType = (BulletType)UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(BulletType)).Length);
        }

        void OnBecameInvisible()
        {
            Destroy(gameObject);
        }

    }
}
