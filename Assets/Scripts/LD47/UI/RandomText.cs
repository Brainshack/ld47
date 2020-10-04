using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LD47.UI
{
    public class RandomText : MonoBehaviour
    {
        public string[] texts;

        public TMP_Text text;

        private void Start()
        {
            UpdateText();
        }

        public void UpdateText()
        {
            text.text = texts[Random.Range(0, texts.Length)];
        }
    }
}