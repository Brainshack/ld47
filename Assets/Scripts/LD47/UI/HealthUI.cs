using System;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using static TMPro.ShaderUtilities;

namespace LD47.UI
{
    public class HealthUI : MonoBehaviour
    {
        public Gradient healthColors;

        public TMP_Text healthText;

        public Image healthIcon;

        private Health _healthComponent;

        private GameEvents _gameEvents;

        private void Start()
        {
            _gameEvents = GameEvents.Instance;
            _gameEvents.OnPlayerSpawn.AddListener((player => { _healthComponent = player.GetComponent<Health>(); }));
        }

        void Update()
        {
            if (_healthComponent == null) return;
            
            var color = healthColors.Evaluate((float) _healthComponent.CurrentHealth /
                                              (float) _healthComponent.maxHealth);
            healthText.color = color;
            healthIcon.color = color;
            healthText.fontSharedMaterial.SetColor(ID_GlowColor, color);
            healthText.text = _healthComponent.CurrentHealth.ToString();
        }
    }
}