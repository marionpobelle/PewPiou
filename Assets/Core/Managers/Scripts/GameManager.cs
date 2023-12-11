using Nano.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nano.Data;
using Nano.UI;

namespace Nano.Managers
{
    public class GameManager : MonoBehaviour
    {
        //magic number : TODO proper max player amount to start game
        private const int MAX_PLAYER_AMOUNT = 2;

        public static GameManager Instance;
        public static event Action onGameStart;

        private List<PlayerEntity> players = new List<PlayerEntity>();

        [Header("LEVEL")]
        public List<GameObject> enemyList = new List<GameObject>();
        public LevelScriptable currentLevel;
        float levelTimer;
        int phaseNumber = 0;

        void Awake()
        {
            PlayerJoinManager.OnPlayerAdded += OnPlayerAdded;
        }

        private void Update()
        {
            levelTimer += Time.deltaTime;
            if (phaseNumber < currentLevel.phaseList.Count && levelTimer >= currentLevel.phaseList[phaseNumber].startTime)
            {
                SpawnPhase(phaseNumber);
            }
            else if (phaseNumber >= currentLevel.phaseList.Count && levelTimer >= currentLevel.levelTime) //AND NO ENEMY FOUND 
            {
                GameOver();
            }
        }

        private void SpawnPhase(int _phaseNumber)
        {
            phaseNumber++;
            float _waitBetweenSpawnTime = 0f;
            for (int i = 0; i < currentLevel.phaseList[_phaseNumber].enemyEntryList.Count; i++)
            {
                for (int j = 0; j < currentLevel.phaseList[_phaseNumber].enemyEntryList[i].number; j++)
                {
                    StartCoroutine(WaitSpawnEnemy(currentLevel.phaseList[_phaseNumber].enemyEntryList[i].enemyType, _waitBetweenSpawnTime));
                    _waitBetweenSpawnTime += currentLevel.phaseList[_phaseNumber].timeBetweenEachSpawn;
                }
            }
        }

        IEnumerator WaitSpawnEnemy(LevelScriptable.EnemyType _enemyType, float _wait)
        {
            yield return new WaitForSeconds(_wait);
            SpawnEnemy(_enemyType);
        }

        private void SpawnEnemy(LevelScriptable.EnemyType _enemyType)
        {
            Instantiate(enemyList[(int)_enemyType]);
        }

        private void OnPlayerAdded(PlayerEntity newPlayer)
        {
            players.Add(newPlayer);

            ScoreUI.Instance.AddPlayer(newPlayer.playerData);

            if (players.Count == MAX_PLAYER_AMOUNT)
            {
                StartGame();
            }
        }

        private void StartGame()
        {
            Debug.Log($"{this.GetType()} >> Starting Game");

            onGameStart?.Invoke();
        }

        private void GameOver()
        {

        }
    }
}