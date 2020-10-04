using System;
using UnityEngine;
using UnityEngine.Events;

namespace LD47
{
    public class GameEvents
    {
        private static GameEvents _instance;

        public static GameEvents Instance
        {
            get {
                if (_instance == null) _instance = new GameEvents();
                return _instance;
            }
        }

        public UnityEvent<int> OnPlayerDamage = new UnityEvent<int>();
        public UnityEvent OnGameOver = new UnityEvent();

        public UnityEvent<int> OnGameSetup = new UnityEvent<int>();
        public UnityEvent OnAfterLevelGeneration = new UnityEvent();
        public UnityEvent OnGameplayStart = new UnityEvent();
    }
}