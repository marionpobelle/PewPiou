using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nano.UI
{
    public class JoinCanvas : MonoBehaviour
    {
        [SerializeField] List<GameObject> pannels;

        public void RemoveAToJoin()
        {
            if (pannels.Count > 0)
            {
                pannels[0].SetActive(false);
                pannels.RemoveAt(0);
            }
        }
    }
}