using System;
using TMPro;
using UnityEngine;

namespace LD47.UI
{
    public class ScoreUI : MonoBehaviour
    {
        public TMP_Text highScoreText;

        private int score;

        private GameEvents _events;

        private void Awake()
        {
            _events = GameEvents.Instance;
            _events.OnEnemyDeath.AddListener(() =>
            {
                score += 10;
                highScoreText.text = score.ToString();
            });
        }
    }
}