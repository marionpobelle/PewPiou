using Nano.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nano.Entity
{
    public class PlayerEntity : Entity
    {
        //casts our data as player data
        public PlayerData playerData => (PlayerData)Data;

        protected override void Start()
        {
            Data = new PlayerData();
        }
    }
}