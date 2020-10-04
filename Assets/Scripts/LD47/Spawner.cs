using UnityEngine;

namespace LD47
{
    public class Spawner : MonoBehaviour
    {
        public GameObject enemyPrefab;
        public GameObject playerPrefab;
        
        public GameObject SpawnEnemy(Vector3 position)
        {
            return Instantiate(enemyPrefab, position, transform.rotation, transform);
        }
        
        public GameObject SpawnPlayer(Vector3 position)
        {
            return Instantiate(playerPrefab, position, transform.rotation, transform);
        }
    }
}