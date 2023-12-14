using Nano.UI;
using Nano.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nano.Entity;
using Nano.Level;
using Sirenix.OdinInspector;

namespace Nano.Managers
{
    public class GameManager : MonoBehaviour
    {
        //magic number : TODO proper max player amount to start game
        private const int MAX_PLAYER_AMOUNT = 2;

        public static GameManager Instance;
        public static event Action onGameStart;

        private List<PlayerEntity> players = new List<PlayerEntity>();
        bool isGameRunning = false;

        [Header("LEVEL")]
        [SerializeField] List<Transform> spawnPositions;
        public List<GameObject> enemyList = new List<GameObject>();
        public LevelScriptable currentLevel;
        [SerializeField] BackgroundManager backgroundManager;
        float levelTimer;
        int phaseNumber = 0;
        bool isGamePaused = false;
        [SerializeField] MainCameraLayerHandler camHandler;
        [Header("SPAWN ZONE")]
        [SerializeField] float height;

        [Header("UI")]
        [SerializeField] JoinCanvas joinCanvas;
        [SerializeField] GameOverScreen gameOverScreen;
        [SerializeField] PauseMenu pauseMenu;

        [Header("Audio")]
        [SerializeField] AK.Wwise.Event pauseBGM;
        [SerializeField] AK.Wwise.Event resumeBGM;
        [SerializeField] AK.Wwise.Event UiMenuSelect_00_SFX;
        [SerializeField] AK.Wwise.Event UiMenuBack_00_SFX;
        [SerializeField] AK.Wwise.Event BGM_stopall;
        [SerializeField] AK.Wwise.Event BGM_Main;
      


        [HideInInspector] public bool onePlayerStart;
        [HideIf("onePlayerStart")]
        [BoxGroup("DEBUG", ShowLabel = true)]
        [Button]
        public void OnePlayerModeDeactivated () { onePlayerStart = true; }
        [ShowIf("onePlayerStart")]
        [BoxGroup("DEBUG", ShowLabel = true)]
        [Button, GUIColor(0f, 1f, 0f)]
        public void OnePlayerModeActivated() { onePlayerStart = false; }

        void Awake()
        {
            PlayerJoinManager.OnPlayerAdded += OnPlayerAdded;
            InputHandler.onPausePressed += PauseInput;
            Time.timeScale = 1; 
            OnBackgroundOptionChanged();
            OptionsMenu.onShowBackgroundValueChanged += OnBackgroundOptionChanged;
        }

        private void OnDestroy()
        {
            InputHandler.onPausePressed -= PauseInput;
            PlayerJoinManager.OnPlayerAdded -= OnPlayerAdded;
            OptionsMenu.onShowBackgroundValueChanged -= OnBackgroundOptionChanged;
            BGM_stopall.Post(gameObject);
        }

        private void Update()
        {
            if (!isGameRunning) return;

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
                            _position = new Vector2(100, height / 2 - (height / (_spawnNumber - 1) * _spawnStep) + transform.position.y);
                            break;
                        case LevelScriptable.SpawnShape.BottomToTop:
                            _position = new Vector2(100, -height / 2 + (height / (_spawnNumber - 1) * _spawnStep) + height / _spawnNumber + transform.position.y);
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

            if (players.IndexOf(newPlayer) < spawnPositions.Count)
                newPlayer.transform.position = spawnPositions[players.IndexOf(newPlayer)].position;
            else
                newPlayer.transform.position = spawnPositions[0].position;

            ScoreUI.Instance.AddPlayer(newPlayer.playerData);

            joinCanvas.RemoveAToJoin();

            newPlayer.playerData.PlayerID = players.IndexOf(newPlayer);

            if (players.Count == MAX_PLAYER_AMOUNT)
            {
                StartGame();
            }
#if UNITY_EDITOR
            else if (onePlayerStart)
            {
                StartGame();
                joinCanvas.RemoveAToJoin();
            }
#endif
        }

        private void StartGame()
        {
            Debug.Log($"{this.GetType()} >> Starting Game");
           
            BGM_Main.Post(gameObject);
            isGameRunning = true;
            backgroundManager.StartBackground();
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

            pauseBGM.Post(gameObject);
            UiMenuBack_00_SFX.Post(gameObject);


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

            resumeBGM.Post(gameObject);
            UiMenuSelect_00_SFX.Post(gameObject);

            foreach (PlayerEntity player in players)
            {
                player.InputHandler.UnfreezeInputs();
            }
        }

        private void GameOver()
        {
            isGameRunning = false;

            foreach (PlayerEntity player in players)
            {
                player.InputHandler.FreezeInputs();
            }

            //TODO : tie handling. I am NOT doing this now
            gameOverScreen.PlayAnimation(players[0].playerData.score > players[1].playerData.score);
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(1, 0, 0, 0.5f);
            Gizmos.DrawCube(new Vector3(25, transform.position.y, 0), new Vector3(50, height, 1));
        }

        void OnBackgroundOptionChanged()
        {
            camHandler.SetBackgroundVisibility(isBackgroundVisible);
        }

        //dirty af
        bool isBackgroundVisible => PlayerPrefs.GetInt("IsBackgroundDisabled") == 0;    }
}