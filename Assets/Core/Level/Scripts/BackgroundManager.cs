using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nano.Data;

namespace Nano.Level
{
    public class BackgroundManager : MonoBehaviour
    {
        [SerializeField] List<BackgroundPhase> phases;
        [SerializeField] float xSpawnPos;
        [SerializeField] float ySpawnPos;

        int currentPhaseIndex;
        float nextPhaseTime;
        bool isBackgroundRunning;

        private void Start()
        {
            StartBackground();
        }

        private void StartBackground()
        {
            currentPhaseIndex = 0;
            SetupBackground();
            isBackgroundRunning = true;
        }

        

        private void Update()
        {
            if (!isBackgroundRunning)
                return;

            if (Time.time > nextPhaseTime)
            {
                currentPhaseIndex++;

                if (currentPhaseIndex == phases.Count)
                {
                    Debug.Log($"{nameof(BackgroundManager)} >> last Phases ended. Stopping background");
                    isBackgroundRunning = false;
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
                        Random.Range(-backgroundElement.Variation, backgroundElement.Variation);

                    BackgroundProp newProp = InstantiateBackgroundProp(backgroundElement).GetComponent<BackgroundProp>();

                    newProp.Init(GetPropSpeed(backgroundElement, newProp.transform));
                    newProp.transform.localScale = GetRandomUniformVector3(backgroundElement.MinMaxScale);
                }
            }
        }

        private static float GetPropSpeed(BackgroundElement backgroundElement, Transform newPropTransform)
        {
            float propSpeed = backgroundElement.UseAutoSpeed ? (1/ newPropTransform.position.z ) * backgroundElement.SpeedByDistanceRatio : Random.Range(backgroundElement.MinMaxSpeed.x, backgroundElement.MinMaxSpeed.y);
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
            float randomScale = Random.Range(minMaxScale.x, minMaxScale.y);

            return new Vector3(randomScale, randomScale, randomScale);
        }

        private GameObject InstantiateBackgroundProp(BackgroundElement backgroundElement) =>
            Instantiate(backgroundElement.Prefab, GetSpawnPosition(backgroundElement), Quaternion.identity);

        private Vector3 GetSpawnPosition(BackgroundElement backgroundElement) =>
            new Vector3(xSpawnPos, ySpawnPos, Random.Range(backgroundElement.MinMaxSpawnDistance.x, backgroundElement.MinMaxSpawnDistance.y));
    }
}