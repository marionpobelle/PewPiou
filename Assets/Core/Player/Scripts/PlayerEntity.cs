using Nano.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nano.Player
{
    public class PlayerEntity : Entity.Entity
    {
        //casts our data as player data
        public PlayerData playerData => (PlayerData)Data;
        public SquadronManager squadronManager;
        public InputHandler InputHandler => inputHandler;

        [SerializeField] private InputHandler inputHandler;

        protected override void Awake()
        {
            Data = new PlayerData();
        }
    }
}