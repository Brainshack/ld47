using System;
using UnityEngine;

namespace LD47
{
    public class AudioManager : MonoBehaviour
    {
        
        public int maxAudioSources = 64;

        private int currentPlayer = 0;

        private GameObject[] audioPlayers;

        public GameObject audioPlayerPrefab;
        
        public void PlaySound(AudioClip clip, Vector3 pos, bool isPlayerWeaponSound = false)
        {
            var player = audioPlayers[currentPlayer];

            player.transform.position = pos;
            var src = player.GetComponent<AudioSource>();
            if (isPlayerWeaponSound)
                src.volume = 0.05f;
            else
                src.volume = 1f;
            
            src.PlayOneShot(clip);

            currentPlayer++;
            if (currentPlayer >= maxAudioSources-1) currentPlayer = 0;
        }
        
        private void Awake()
        {
            audioPlayers = new GameObject[maxAudioSources];
            for (int i = 0; i < maxAudioSources; i++)
            {
                audioPlayers[i] = Instantiate(audioPlayerPrefab, transform);
            }
            
        }
        
        
    }
}