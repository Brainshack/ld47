using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LD47.UI
{
    public class LevelStartScreenUI : MonoBehaviour
    {
        public TMP_Text stageText;

        private GameRules _gameRules;

        private GameEvents _gameEvents;

        public GameObject UIObject;
        private void Start()
        {
            _gameRules = GameRules.Instance;
            _gameEvents = GameEvents.Instance;
            
            _gameEvents.OnGameSetup.AddListener((int seed) =>
            {
                Cursor.lockState = CursorLockMode.None;
                UIObject.SetActive(true);
                stageText.text = $"Stage 1-{_gameRules.stage}";
            });
            
            UIObject.SetActive(false);
        }

        public void Close()
        {
            Cursor.lockState = CursorLockMode.Locked;
            UIObject.SetActive(false);
            _gameEvents.OnGameplayStart.Invoke();
        }
    }
}