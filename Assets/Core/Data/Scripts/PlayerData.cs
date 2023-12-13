using System.Collections.Generic;
using UnityEngine;

namespace Nano.Data
{
    public class PlayerData : EntityData
    {
        public Vector2 CurrentInput { get => currentInput; set => currentInput = value ; }
        public int PlayerID { get => playerID; set => playerID = value; }

        private Vector2 currentInput;
        private int playerID;

        public List<BulletType> shieldTypeList = new List<BulletType>();

        public float score;
    }
}