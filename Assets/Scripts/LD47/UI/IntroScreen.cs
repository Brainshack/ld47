using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LD47.UI
{
    public class IntroScreen : MonoBehaviour
    {
        private void Start()
        {
            Invoke("Load", 10f);
        }

        void Load()
        {
            SceneManager.LoadScene("Main");
        }
    }
}