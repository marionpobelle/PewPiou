using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Nano.UI
{
    public class GameOverScreen : MonoBehaviour
    {
        [SerializeField] Button restartButton;
        [SerializeField] Button titleButton;
        [SerializeField] CanvasGroup group1;
        [SerializeField] CanvasGroup group2;

        [SerializeField] GameObject player1Title;
        [SerializeField] GameObject player2Title;

        [SerializeField] float finnishedFadeDuration = .3f;
        [SerializeField] float finnishedShowDuration = 1f;
        [SerializeField] float playerFadeDuration = .3f;

        public static event Action onRestart;
        public static event Action onTitle;

        private void Awake()
        {
            restartButton.onClick.AddListener(RestartButton);
            titleButton.onClick.AddListener(TitleButton);
        }

        private void OnDestroy()
        {
            restartButton.onClick.RemoveListener(RestartButton);
            titleButton.onClick.RemoveListener(TitleButton);
        }

        private void RestartButton()
        {
            group2.interactable = false;
            onRestart?.Invoke();
        }

        private void TitleButton()
        {
            group2.interactable = false;
            onTitle?.Invoke();
        }

        [Button]
        public void PlayAnimation(bool isPlayer1Winner = true)
        {
            (isPlayer1Winner ? player1Title : player2Title).SetActive(true);
            Sequence sequence = DOTween.Sequence();
            sequence.Append(group1.DOFade(1,finnishedFadeDuration));
            sequence.AppendInterval(finnishedShowDuration);
            sequence.Append(group1.DOFade(0, finnishedFadeDuration));
            sequence.Append(group2.DOFade(1, playerFadeDuration));
            sequence.AppendCallback(() => group2.interactable = true);
            sequence.AppendCallback(() =>restartButton.Select());
        }
    }
}