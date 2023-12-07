using Nano.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nano.Managers
{
    public class GameManager : MonoBehaviour
    {
        //magic number : TODO proper max player amount to start game
        private const int MAX_PLAYER_AMOUNT = 2;

        public static GameManager Instance;
        public static event Action onGameStart;

        private List<PlayerEntity> players = new List<PlayerEntity>();

        void Awake()
        {
            PlayerJoinManager.OnPlayerAdded += OnPlayerAdded;
        }

        private void OnPlayerAdded(PlayerEntity newPlayer)
        {
            players.Add(newPlayer);

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
    }
}