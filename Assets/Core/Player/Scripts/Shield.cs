using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nano.Data;
using Sirenix.OdinInspector;

namespace Nano.Player
{
    public class Shield : MonoBehaviour
    {
        [BoxGroup("COMPONENTS", ShowLabel = true)]
        public MeshRenderer shieldRenderer;
        [Space]
        public BulletType shieldType;
        [HideInInspector] public float fixedScale;
    }
}

