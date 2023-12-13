using Nano.Level;
using Sirenix.OdinInspector;
using UnityEngine;

public class BackgroundPlane : BackgroundProp
{
    [SerializeField] float sunsetDuration = 10;
    bool isSunsetBlending;

    float startSunsetTime;
    float endSunseTime;

    protected override void Awake()
    {
        base.Awake();
        mpb = new MaterialPropertyBlock();
        BackgroundManager.onSunsetBlendStart += SunsetBlend;
    }

    private void OnDestroy()
    {
        BackgroundManager.onSunsetBlendStart -= SunsetBlend;
    }

    [Button]
    private void SunsetBlend()
    {
        isSunsetBlending = true;
        startSunsetTime = Time.time;
        endSunseTime = Time.time+sunsetDuration;
    }

    protected override void Update()
    {
        base.Update();

        if (isSunsetBlending)
        {
            mpb.SetFloat("_sunset", Mathf.InverseLerp(endSunseTime, startSunsetTime, Time.time));
            meshRenderer.SetPropertyBlock(mpb);
        }
    }
}
