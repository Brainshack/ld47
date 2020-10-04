using System;
using System.Collections;
using UnityEngine;

namespace LD47
{
    public class GameRules : MonoBehaviour
    {
        private GameEvents _events;

        private Player _player;

        private static GameRules _instance;

        private int _seed;

        public static GameRules Instance => _instance;

        private void Awake()
        {
            if (_instance == null) _instance = this;
        }

        private void Start()
        {
            _events = GameEvents.Instance;

            StartCoroutine(nameof(StartGame));
        }
        private IEnumerator StartGame()
        {
            yield return new WaitForSecondsRealtime(0.1f);
            _events.OnAfterLevelGeneration.AddListener(() =>
            {
                _player = FindObjectOfType<Player>();

                _player.Health.OnDeath.AddListener(() =>
                {
                    Time.timeScale = 0;
                    _events.OnGameOver.Invoke();
                });
            });
            
            _events.OnPlayerEnteredExit.AddListener(() =>
            {
                nextLevel();
            });

            nextLevel();
        }
        
        public void nextLevel()
        {
            _seed = (int) (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            startLevel(_seed);
        }

        public void restartLevel()
        {
            startLevel(_seed);
        }

        private void startLevel(int seed)
        {
            Time.timeScale = 1;
            _events.OnGameSetup.Invoke(_seed);
        }
    }
}