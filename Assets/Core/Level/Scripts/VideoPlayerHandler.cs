using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

namespace Nano.Level
{
    public class VideoPlayerHandler : MonoBehaviour
    {
        VideoPlayer videoPlayer;
        bool isBlending = false;

        protected MaterialPropertyBlock mpb;
        protected MeshRenderer meshRenderer;

        private void Awake()
        {
            videoPlayer = GetComponent<VideoPlayer>();
            BackgroundManager.onAlphaBlendStart += AlphaBlend;

            meshRenderer = GetComponent<MeshRenderer>();

            mpb = new MaterialPropertyBlock();
            meshRenderer.GetPropertyBlock(mpb);
        }

        private void OnDestroy()
        {
            BackgroundManager.onAlphaBlendStart -= AlphaBlend;
        }

        void AlphaBlend()
        {
            videoPlayer.Play();
            isBlending = true;
        }

        private void Update()
        {
            if (!isBlending)
                return;

            mpb.SetColor("_BaseColor", new Color(1, 1, 1, 1- BackgroundManager.BlendValue()));
            meshRenderer.SetPropertyBlock(mpb);
        }
    }
}