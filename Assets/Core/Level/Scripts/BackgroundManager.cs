using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using Nano.Data;
using UnityEngine.UIElements;
using System;

namespace Nano.Level
{
    public class BackgroundManager : MonoBehaviour
    {
        [SerializeField] List<BackgroundPhase> phases;
        [SerializeField] float xSpawnPos;
        [SerializeField] float ySpawnPos;
        [SerializeField] float startSunsetDelay = 140;
        [SerializeField] float alphaBlendDuration = 5;

        float startSunsetTime;
        int currentPhaseIndex;
        float nextPhaseTime;
        bool isBackgroundRunning;
        public static event Action onAlphaBlendStart;
        public static event Action onSunsetBlendStart;

        bool isSunsetStarted;

        public static float startBlendTime;
        public static float endBlendTime;
        public static float BlendValue() { return Mathf.InverseLerp(endBlendTime, startBlendTime, Time.time); }

        private void Start()
        {
            //StartBackground();
        }

        public void StartBackground()
        {
            startSunsetTime = Time.time + startSunsetDelay;
            currentPhaseIndex = 0;
            SetupBackground();
            isBackgroundRunning = true;
        }

        private void Update()
        {
            if (!isBackgroundRunning)
                return;

            if (!isSunsetStarted && Time.time > startSunsetTime)
            {
                isSunsetStarted = true;
                onSunsetBlendStart?.Invoke();
            }

            if (Time.time > nextPhaseTime)
            {
                currentPhaseIndex++;

                if (currentPhaseIndex == phases.Count)
                {
                    Debug.Log($"{nameof(BackgroundManager)} >> last Phases ended. Stopping background");
                    isBackgroundRunning = false;
                    StartAlphaEvent();
                    return;
                }
                else
                {
                    SetupBackground();
                }
            }

            foreach (var backgroundElement in phases[currentPhaseIndex].BackgroundElements)
            {
                if (backgroundElement.NextSpawnTime <= Time.time)
                {
                    backgroundElement.NextSpawnTime = Time.time + backgroundElement.AverageSpawnTime +
                        UnityEngine.Random.Range(-backgroundElement.Variation, backgroundElement.Variation);

                    BackgroundProp newProp = InstantiateBackgroundProp(backgroundElement).GetComponent<BackgroundProp>();

                    newProp.Init(GetPropSpeed(backgroundElement, newProp.transform));
                    newProp.transform.localScale = backgroundElement.UseAutoScale ?
                        GetPropScaleByDistance(backgroundElement, newProp.transform) :
                        GetRandomUniformVector3(backgroundElement.MinMaxScale);
                }
            }
        }

        private void StartAlphaEvent()
        {
            onAlphaBlendStart?.Invoke();
            startBlendTime = Time.time;
            endBlendTime = Time.time + alphaBlendDuration;
        }

        private Vector3 GetPropScaleByDistance(BackgroundElement backgroundElement, Transform newPropTransform)
        {
            float scale = (1 / newPropTransform.position.z) * backgroundElement.ScaleByDistanceMultiplier;

            float randomValue = ((.1f) * UnityEngine.Random.Range(-backgroundElement.RandomScaleMultiplierByDistance, backgroundElement.RandomScaleMultiplierByDistance));

            scale += scale * randomValue;

            return new Vector3(scale, scale, scale);
        }

        private static float GetPropSpeed(BackgroundElement backgroundElement, Transform newPropTransform)
        {
            float propSpeed = backgroundElement.UseAutoSpeed ? (1 / newPropTransform.position.z) * backgroundElement.SpeedByDistanceRatio : UnityEngine.Random.Range(backgroundElement.MinMaxSpeed.x, backgroundElement.MinMaxSpeed.y);
            return propSpeed;
        }

        private void SetupBackground()
        {
            nextPhaseTime = Time.time + phases[currentPhaseIndex].PhaseDuration;

            foreach (var backgroundElement in phases[currentPhaseIndex].BackgroundElements)
            {
                backgroundElement.NextSpawnTime = Time.time + backgroundElement.StartupDelay;
            }
        }

        private Vector3 GetRandomUniformVector3(Vector2 minMaxScale)
        {
            float randomScale = UnityEngine.Random.Range(minMaxScale.x, minMaxScale.y);

            return new Vector3(randomScale, randomScale, randomScale);
        }

        private GameObject InstantiateBackgroundProp(BackgroundElement backgroundElement) =>
            Instantiate(backgroundElement.Prefab, GetSpawnPosition(backgroundElement), Quaternion.identity, transform);

        private Vector3 GetSpawnPosition(BackgroundElement backgroundElement) =>
            new Vector3(xSpawnPos, ySpawnPos, UnityEngine.Random.Range(backgroundElement.MinMaxSpawnDistance.x, backgroundElement.MinMaxSpawnDistance.y));



        [Button]
        void TestAlphaBlending()
        {
            isBackgroundRunning = false;
            StartAlphaEvent();
        }
    }
}