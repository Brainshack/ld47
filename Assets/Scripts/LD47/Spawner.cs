using UnityEngine;

namespace LD47
{
    public class Spawner : MonoBehaviour
    {
        public GameObject enemyPrefab;
        
        public GameObject SpawnEnemy(Vector3 position)
        {
            return Instantiate(enemyPrefab, position, transform.rotation, transform);
        }
    }
}