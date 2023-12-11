using System.Collections.Generic;
using UnityEngine;

namespace Nano.Data
{
    public class PlayerData : EntityData
    {
        public Vector2 CurrentInput { get => currentInput; set => currentInput = value ; }

        private Vector2 currentInput;

        public List<BulletType> shieldTypeList = new List<BulletType>();
    }
}