using Nano.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nano.Entity
{
    public class Entity : MonoBehaviour
    {
        private EntityData data;

        public EntityData Data { get => data; set => data = value; }

        protected virtual void Awake()
        {
            Data = new EntityData();
        }
    }
}