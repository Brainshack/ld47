using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LD47
{
    public class ObjectiveTracker : MonoBehaviour
    {

        private TMP_Text enemyCountDisplay;
        public TMP_Text objectiveMetText;

        public GameObject objectiveTextPrefab;

        public Transform objectiveContainerTransform;

        private GameEvents _events;

        private int _enemyThreshold = 0;
        private int _aliveEnemies = 0;

        private bool _allObjectivesMet = false;
        
        private void Awake()
        {
            _events = GameEvents.Instance;
            _events.OnGameplayStart.AddListener(InitObjectives);
        }

        public void InitObjectives()
        {
            _allObjectivesMet = false;
            _aliveEnemies = FindObjectsOfType<Enemy>().Length;
            
            var childCount = objectiveContainerTransform.childCount;
            for (int i = childCount - 1; i > 0; i--)
            {
                Destroy(objectiveContainerTransform.GetChild(i).gameObject);
            }

            _enemyThreshold = Random.Range(_aliveEnemies / 4, _aliveEnemies /2);

            if (!enemyCountDisplay)
            {
                var enemyTextGo = Instantiate(objectiveTextPrefab, objectiveContainerTransform);
                enemyCountDisplay = enemyTextGo.GetComponent<TMP_Text>();    
            }
            
            UpdateEnemyText();
        }

        private void Update()
        {
            if (!enemyCountDisplay) return;
            
            _aliveEnemies = FindObjectsOfType<Enemy>().Length;

            if (!_allObjectivesMet)
            {
                _allObjectivesMet = CheckObjectives();

                if (_allObjectivesMet)
                {
                    _events.OnAllObjectivesMet.Invoke();
                }
            }
            
            UpdateEnemyText();
            UpdateObjectiveText();
        }

        private void UpdateObjectiveText()
        {
            if (!_allObjectivesMet)
            {
                objectiveMetText.color = Color.green;
                objectiveMetText.text = "TRUE";
            }
            else
            {
                objectiveMetText.color = Color.red;
                objectiveMetText.text = "FALSE";
            }
        }
        
        void UpdateEnemyText()
        {
            if (enemyCountDisplay)
            {
                enemyCountDisplay.text = $"#Enemies > {_enemyThreshold}";

                if (_aliveEnemies > _enemyThreshold)
                {
                    enemyCountDisplay.color = Color.green;
                }
                else
                {
                    enemyCountDisplay.color = Color.red;
                }
            }
        }

        private bool CheckObjectives()
        {
            return !(_aliveEnemies > _enemyThreshold);
        }
    }
}