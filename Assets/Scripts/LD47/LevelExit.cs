using System;
using UnityEngine;

namespace LD47
{
    public class LevelExit : MonoBehaviour
    {
        private GameEvents _events;
        
        private void Awake()
        {
            _events = GameEvents.Instance;
            
            gameObject.SetActive(false);
            
            _events.OnAllObjectivesMet.AddListener(() =>
            {
                gameObject.SetActive(true);
            });
        }

        private void OnTriggerEnter(Collider other)
        {
            
            if (other.CompareTag("Player"))
            {
                gameObject.SetActive(false);
                _events.OnPlayerEnteredExit.Invoke();        
            }
        }
    }
}