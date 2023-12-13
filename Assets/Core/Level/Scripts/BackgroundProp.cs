using Sirenix.OdinInspector;
using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Nano.Level
{
    public class BackgroundProp : MonoBehaviour
    {
        bool isBlending = false;
        float speed;

        [SerializeField] protected MeshRenderer meshRenderer;
        protected MaterialPropertyBlock mpb;

        public void Init(float speed)
        {
            this.speed = speed;
        }

        protected virtual void Awake()
        {
            BackgroundManager.onAlphaBlendStart += AlphaBlend;
        }

        private void OnDestroy()
        {
            BackgroundManager.onAlphaBlendStart -= AlphaBlend;
        }

        private void AlphaBlend()
        {
            if (meshRenderer == null)
                return;
            isBlending = true;
            mpb = new MaterialPropertyBlock();
            meshRenderer.GetPropertyBlock(mpb);
        }

        protected virtual void Update()
        {
            if (transform.position.x < -200)
            {
                Destroy(gameObject);
            }

            transform.position += Vector3.left * speed * Time.deltaTime;

            if (isBlending)
            {
                mpb.SetFloat("_blend", BackgroundManager.BlendValue());
                meshRenderer.SetPropertyBlock(mpb);
            }
        }

        [Button]
        void AssignMeshRenderer()
        {
            meshRenderer = GetComponentInChildren<MeshRenderer>();
        }
    }
}