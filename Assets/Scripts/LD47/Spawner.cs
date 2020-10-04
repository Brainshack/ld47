using System;
using System.Collections.Generic;
using UnityEngine;

namespace LD47
{
    public class Spawner : MonoBehaviour
    {
        public GameObject enemyPrefab;
        public GameObject playerPrefab;
        public GameObject exitPrefab;

        private List<GameObject> _enemies = new List<GameObject>();

        private GameObject _player;
        private GameObject _exit;

        private GameEvents _gameEvents;

        private void Start()
        {
            _gameEvents = GameEvents.Instance;
        }

        public void ClearEnemies()
        {
            foreach (var enemy in _enemies)
            {
                Destroy(enemy);
            }
            
            _enemies = new List<GameObject>();
        }
        
        public GameObject SpawnEnemy(Vector3 position)
        {
            var enemy = Instantiate(enemyPrefab, position, transform.rotation, transform);
            _enemies.Add(enemy);
            return enemy;
        }
        
        public GameObject SpawnPlayer(Vector3 position)
        {
            if (_player == null)
            {
                _player = Instantiate(playerPrefab, position, transform.rotation, transform);
                _exit = Instantiate(exitPrefab, position, transform.rotation, transform);
                _gameEvents.OnPlayerSpawn.Invoke(_player.GetComponent<Player>());
            }
            else
            {
                _player.transform.position = position;
            }
             
            _player.GetComponent<Player>().resetPlayer();

            
            
            var exitPos = new Vector3(position.x, position.y - 1, position.z);
            var rotation = Quaternion.Euler(new Vector3(-90, 0, 0));
            _exit.transform.position = exitPos;
            _exit.transform.rotation = rotation;
            
            return _player;
        }
    }
}