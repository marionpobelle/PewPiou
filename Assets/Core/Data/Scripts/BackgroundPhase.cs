using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nano.Data
{
    [CreateAssetMenu(fileName = "BackgroundPhase_", menuName = "ScriptableObjects/Background Phase")]
    public class BackgroundPhase : ScriptableObject
    {
        [SerializeField, Tooltip("The total duration of the phase. All the phases in the level must add up to 3 minutes.")]
        float phaseDuration;

        [SerializeField, Tooltip("The list of all the elements in the phase.")]
        List<BackgroundElement> backgroundElements;

        public float PhaseDuration { get => phaseDuration; set => phaseDuration = value; }
        public List<BackgroundElement> BackgroundElements { get => backgroundElements; set => backgroundElements = value; }

        List<float>GetDurations(float avgTime, float variation)
        {
            List<float> test = new List<float>();

            float currentTime = 0;

            while (currentTime <= PhaseDuration)
            {
                float chosenDuration = avgTime + UnityEngine.Random.Range(-variation, variation);
                currentTime += chosenDuration;
                test.Add(currentTime);
            }

            return test;
        }
    }

    [Serializable]
    public class BackgroundElement
    {
        [Header("------------------------------------------")]
        [SerializeField][Tooltip("The prefab of the object")] 
        GameObject prefab;

        [Header("Time Settings")][SerializeField]
        [Tooltip("The average duration it takes to spawn this object")] 
        float averageSpawnTime = 5;

        [SerializeField, Tooltip("The variation on the spawn time. The lower it is, the closer to the average spawn time the actual spawn time will be. " +
            "DO NOT SET THIS EQUAL OR HIGHER THAN AVERAGE SMAPWN TIME.")] 
        float variation = 3;

        [SerializeField, Tooltip("The amount of time waited before we can spawn this object when the game starts")] 
        float startupDelay = 0;

        [Header("Distance Settings")]
        [SerializeField][MinMaxSlider(10, 1000)]
        [Tooltip("X is the min distance this can spawn, Y is the max distance it can spawn.")]
        Vector2 minMaxSpawnDistance = new Vector2(30, 200);

        [SerializeField][MinMaxSlider(.5f, 10)]
        [Tooltip("X is the min scale, Y is the max scale.")]
        Vector2 minMaxScale = new Vector2(1, 2);

        [Header("Speed Settings")]
        [SerializeField][MinMaxSlider(0, 10)]
        [Tooltip("X is the min speed, Y is the max speed.")]
        Vector2 minMaxSpeed = new Vector2(.5f, 1.5f);

        float nextSpawnTime = 0f;

        public GameObject Prefab { get => prefab; set => prefab = value; }
        public float AverageSpawnTime { get => averageSpawnTime; set => averageSpawnTime = value; }
        public float Variation { get => variation; set => variation = value; }
        public float StartupDelay { get => startupDelay; set => startupDelay = value; }
        public Vector2 MinMaxSpawnDistance { get => minMaxSpawnDistance; set => minMaxSpawnDistance = value; }
        public Vector2 MinMaxScale { get => minMaxScale; set => minMaxScale = value; }
        public Vector2 MinMaxSpeed { get => minMaxSpeed; set => minMaxSpeed = value; }
        public float NextSpawnTime { get => nextSpawnTime; set => nextSpawnTime = value; }
    }
}