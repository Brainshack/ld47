using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace LD47.UI
{
    public class GameOverUI : MonoBehaviour
    {
        private GameEvents _gameEvents;
        private GameRules _gameRules;


        public Button exitButton;
        public Button restartButton;

        private void Awake()
        {
            _gameEvents = GameEvents.Instance;
            _gameRules = GameRules.Instance;

            Debug.Log("Add  GameOver Listener for Start");
            _gameEvents.OnGameplayStart.AddListener(() =>
            {
                Cursor.lockState = CursorLockMode.Locked;
                gameObject.SetActive(false);    
            });
            
            _gameEvents.OnGameOver.AddListener(() =>
            {
                gameObject.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
            });

            restartButton.onClick.AddListener(() => { _gameRules.restartLevel(); });

            exitButton.onClick.AddListener(() =>
            {
                Application.Quit();
#if UNITY_EDITOR
                EditorApplication.isPlaying = false;
#endif
            });
            
        }
    }
}