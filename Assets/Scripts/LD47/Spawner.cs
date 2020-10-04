using System.Collections.Generic;
using UnityEngine;

namespace LD47
{
    public class Spawner : MonoBehaviour
    {
        public GameObject enemyPrefab;
        public GameObject playerPrefab;

        private List<GameObject> _enemies = new List<GameObject>();

        private GameObject _player;
        
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
            Debug.Log("Spawning Player");
            if (_player == null)
            {
                _player = Instantiate(playerPrefab, position, transform.rotation, transform);
            }
            else
            {
                _player.transform.position = position;
            }
             
            _player.GetComponent<Player>().resetPlayer();
            
            return _player;
        }
    }
}