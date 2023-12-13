using Nano.Entity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Nano.Player
{
    public class PlayerShield : MonoBehaviour
    {
        [SerializeField] PlayerEntity player;
        [SerializeField] SphereCollider shieldCollider;

        [Button("ADD SHIELD")]
        public void AddShield()
        {
            Debug.Log("add shield");
            shieldCollider.radius += 1.0f;
        }
    }

}