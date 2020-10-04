using System.Collections;
using UnityEngine;

namespace LD47.UI
{
    public class HintUI : MonoBehaviour
    {
        private GameEvents _gameEvents;

        public GameObject hintObject;
        
        private void Awake()
        {
            hintObject.SetActive(false);
            _gameEvents = GameEvents.Instance;
            _gameEvents.OnAllObjectivesMet.AddListener(() =>
            {
                StartCoroutine(nameof(Blink));
            });
            
            _gameEvents.OnGameSetup.AddListener((int seed) =>
            {
                StopCoroutine(nameof(Blink));
            });
        }

        IEnumerator Blink()
        {
            while (true)
            {
                hintObject.SetActive(!hintObject.activeSelf);
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}