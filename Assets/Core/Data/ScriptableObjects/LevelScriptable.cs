using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "LevelScriptable", menuName = "ScriptableObjects/LevelScriptable", order = 1)]
public class LevelScriptable : ScriptableObject
{
    public enum EnemyType
    {
        Easy = 0,
        Average = 1,
        Hard = 2
    }

    public enum SpawnShape
    {
        TopToBottom = 0,
        BottomToTop = 1
    }

    [Serializable]
    public class EnemyEntry
    {
        public EnemyType enemyType;
        public int number;

        EnemyEntry(EnemyType _type, int _num)
        {
            enemyType = _type;
            number = _num;
        }
    }

    [Serializable]
    public class Phase
    {
        public float startTime;
        public float timeBetweenEachSpawn;
        public SpawnShape spawnShape;
        public List<EnemyEntry> enemyEntryList = new List<EnemyEntry>();

        Phase (List<EnemyEntry> _entry, float _startTime, float _timeBetweenSpawn, SpawnShape _spawnShape)
        {
            timeBetweenEachSpawn = _timeBetweenSpawn;
            startTime = _startTime;
            spawnShape = _spawnShape;
            enemyEntryList = _entry;
        }
    }

    public float levelTime;
    public List<Phase> phaseList = new List<Phase>();
}
