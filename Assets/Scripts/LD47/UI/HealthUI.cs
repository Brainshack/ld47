﻿using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

namespace LD47.UI
{
    public class HealthUI : MonoBehaviour
    {
        public Gradient healthColors;

        public TMP_Text healthText;

        public Image healthIcon;

        public Health healthComponent;

        void Update()
        {
            var color = healthColors.Evaluate((float) healthComponent.CurrentHealth / (float)healthComponent.maxHealth);
            healthText.color = color;
            healthIcon.color = color;

            healthText.text = healthComponent.CurrentHealth.ToString();
        }
    }
}