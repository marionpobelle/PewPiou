using Nano.UI;
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
        bool isGamePaused = false;
        Vector2 screenBoundX = new Vector2(28, 55);
        Vector2 screenBoundY = new Vector2(-25, 25);

        [Header("UI")]
        [SerializeField] PauseMenu pauseMenu;

        void Awake()
        {
            PlayerJoinManager.OnPlayerAdded += OnPlayerAdded;
            InputHandler.onPausePressed += PauseInput;
        }


        private void OnDestroy()
        {
            InputHandler.onPausePressed -= PauseInput;
            PlayerJoinManager.OnPlayerAdded -= OnPlayerAdded;
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
            int _spawnNumber = 0;
            float range = screenBoundY.y - screenBoundY.x;
            Debug.Log("range is : " + range);
            for (int i = 0; i < currentLevel.phaseList[_phaseNumber].enemyEntryList.Count; i++)
            {
                _spawnNumber += currentLevel.phaseList[_phaseNumber].enemyEntryList[i].number;
            }

            int _spawnStep = 0;
            for (int i = 0; i < currentLevel.phaseList[_phaseNumber].enemyEntryList.Count; i++)
            {
                for (int j = 0; j < currentLevel.phaseList[_phaseNumber].enemyEntryList[i].number; j++)
                {
                    Vector2 _position = Vector2.zero;
                    switch (currentLevel.phaseList[_phaseNumber].spawnShape)
                    {
                        case LevelScriptable.SpawnShape.TopToBottom:
                            _position = new Vector2(100, screenBoundY.y - (range / _spawnNumber * _spawnStep));
                            break;
                        case LevelScriptable.SpawnShape.BottomToTop:
                            _position = new Vector2(100, screenBoundY.x + (range / _spawnNumber * _spawnStep) + range / _spawnNumber);
                            break;
                    }
                    StartCoroutine(WaitSpawnEnemy(currentLevel.phaseList[_phaseNumber].enemyEntryList[i].enemyType, _waitBetweenSpawnTime, _position));
                    _waitBetweenSpawnTime += currentLevel.phaseList[_phaseNumber].timeBetweenEachSpawn;
                    _spawnStep++;
                }
            }
        }

        IEnumerator WaitSpawnEnemy(LevelScriptable.EnemyType _enemyType, float _wait, Vector2 _position)
        {
            yield return new WaitForSeconds(_wait);
            SpawnEnemy(_enemyType, _position);
        }

        private void SpawnEnemy(LevelScriptable.EnemyType _enemyType, Vector2 _position)
        {
            GameObject _enemy = Instantiate(enemyList[(int)_enemyType]);
            _enemy.transform.position = _position;
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

        private void PauseInput()
        {
            if (isGamePaused)
            {
                UnpauseGame();
            }
            else
            {
                PauseGame();
            }
        }

        void PauseGame()
        {
            Time.timeScale = Mathf.Epsilon;
            isGamePaused = true;
            pauseMenu.ShowPauseMenu();
            PauseMenu.onResumeButtonClicked += UnpauseGame;

            foreach (PlayerEntity player in players)
            {
                player.InputHandler.FreezeInputs();
            }
        }

        void UnpauseGame()
        {
            Time.timeScale = 1;
            isGamePaused = false;
            pauseMenu.HidePauseMenu();
            PauseMenu.onResumeButtonClicked -= UnpauseGame;
            foreach (PlayerEntity player in players)
            {
                player.InputHandler.UnfreezeInputs();
            }
        }

        private void GameOver()
        {
            Debug.LogError("Not implemented yet");
        }
    }
}