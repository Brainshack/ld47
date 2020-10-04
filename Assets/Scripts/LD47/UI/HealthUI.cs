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

        public Health healthComponent;

        void Update()
        {
            var color = healthColors.Evaluate((float) healthComponent.CurrentHealth / (float)healthComponent.maxHealth);
            healthText.color = color;
            healthIcon.color = color;
            healthText.fontSharedMaterial.SetColor(ID_GlowColor, color);
            healthText.text = healthComponent.CurrentHealth.ToString();
        }
    }
}