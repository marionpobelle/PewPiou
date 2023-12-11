using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nano.Data;

namespace Nano.UI
{
    public class ScoreUI : MonoBehaviour
    {
        public static ScoreUI Instance;
        [SerializeField] List<ScoreCounter> counters;

        int associatedCounters = 0;
        Dictionary<PlayerData, ScoreCounter> counterMap = new Dictionary<PlayerData, ScoreCounter>();

        private void Awake()
        {
            Instance = this;
        }

        public void AddPlayer(PlayerData playerData)
        {
            ScoreCounter newCounter = counters[associatedCounters];
            associatedCounters++;

            newCounter.Init(playerData, associatedCounters);
            counterMap.Add(playerData, newCounter);
        }

        public void UpdateScore(PlayerData playerData)
        {
            counterMap[playerData].UpdateScore();
        }
    }
}