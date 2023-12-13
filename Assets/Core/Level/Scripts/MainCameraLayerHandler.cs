using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nano.Level
{
    public class MainCameraLayerHandler : MonoBehaviour
    {
        [SerializeField] new Camera camera;
        [SerializeField] LayerMask backgroundLayerMask;
        [SerializeField] LayerMask noBackgroundLayerMask;

        public void SetBackgroundVisibility(bool isVisible)
        {
            camera.cullingMask = isVisible ? backgroundLayerMask : noBackgroundLayerMask;
        }
    }
}