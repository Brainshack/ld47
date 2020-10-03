using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace LD47
{
    public class LevelGenerator : MonoBehaviour
    {
        [FormerlySerializedAs("WallPrefab")] public GameObject wallPrefab;

        [FormerlySerializedAs("FloorPrefab")] public GameObject floorPrefab;

        [FormerlySerializedAs("WalkerCount")] [Tooltip("Amount of Walkers being spawned to dig a path")]
        public int walkerCount = 2;

        [FormerlySerializedAs("Seed")] public int seed = 1337;

        [FormerlySerializedAs("MapSize")] [Tooltip("Map Size squared")]
        public int mapSize = 32;

        private enum MapTile
        {
            Void,
            Wall,
            Floor
        }

        private MapTile[,] _tileMap;

        private int _maxSteps;

        private void Awake()
        {
            _tileMap = new MapTile[mapSize, mapSize];
            for (int x = 0; x < mapSize; x++)
            {
                for (int y = 0; y < mapSize; y++)
                {
                    _tileMap[x, y] = MapTile.Void;
                }
            }

            _maxSteps = (mapSize * mapSize) / 2;
        }

        public void Generate()
        {
#if UNITY_EDITOR
            Awake();
#endif

            Random.InitState(seed);

            var childCount = transform.childCount;
            for (int i = childCount - 1; i > 0; i--)
            {
                DestroyImmediate(transform.GetChild(i).gameObject);
            }

            var parent = transform;

            SpawnWalkers();
            AddWalls();
            for (int x = 0; x < mapSize; x++)
            {
                for (int y = 0; y < mapSize; y++)
                {
                    switch (_tileMap[x, y])
                    {
                        case MapTile.Floor:
                            Instantiate(floorPrefab, new Vector3(x, 0, y), parent.rotation, parent);
                            break;
                        case MapTile.Wall:
                            Instantiate(wallPrefab, new Vector3(x, 0, y), parent.rotation, parent);
                            break;
                    }
                }
            }

            Debug.Log($"We now have {transform.childCount} children");
        }

        private void AddWalls()
        {
            for (int x = 0; x < mapSize; x++)
            {
                for (int y = 0; y < mapSize; y++)
                {
                    
                    if (x == 0 || x == mapSize - 1 || y == 0 || y == mapSize - 1)
                    {
                        _tileMap[x, y] = MapTile.Wall;
                        continue;
                    }
                    
                    if (_tileMap[x, y] != MapTile.Void) continue;

                    if (
                        _tileMap[x - 1, y] == MapTile.Floor
                        || _tileMap[x + 1, y] == MapTile.Floor
                        || _tileMap[x, y - 1] == MapTile.Floor
                        || _tileMap[x, y + 1] == MapTile.Floor
                        || _tileMap[x - 1, y - 1] == MapTile.Floor
                        || _tileMap[x + 1, y + 1] == MapTile.Floor
                        || _tileMap[x - 1, y + 1] == MapTile.Floor
                        || _tileMap[x + 1, y - 1] == MapTile.Floor
                    )
                    {
                        _tileMap[x, y] = MapTile.Wall;
                    }
                }
            }
        }

        private void SpawnWalkers()
        {
            Vector2Int[] walkerPosition = new Vector2Int[walkerCount];
            Vector2Int[] walkerDirection = new Vector2Int[walkerCount];

            for (int j = 0; j < walkerCount; j++)
            {
                walkerPosition[j] = new Vector2Int(mapSize / 2, mapSize / 2);
                if (Random.Range(0, 1) == 0)
                {
                    if (Random.Range(0, 1) == 0)
                    {
                        walkerDirection[j] = Vector2Int.up;
                    }
                    else
                    {
                        walkerDirection[j] = Vector2Int.down;
                    }
                }
                else
                {
                    if (Random.Range(0, 1) == 0)
                    {
                        walkerDirection[j] = Vector2Int.left;
                    }
                    else
                    {
                        walkerDirection[j] = Vector2Int.right;
                    }
                }
            }

            for (int i = 0; i < _maxSteps; i++)
            {
                for (int j = 0; j < walkerCount; j++)
                {
                    var currentPos = walkerPosition[j];

                    _tileMap[currentPos.x, currentPos.y] = MapTile.Floor;

                    List<Vector2Int> availableDirections = new List<Vector2Int>()
                    {
                        Vector2Int.down, Vector2Int.left, Vector2Int.right, Vector2Int.up
                    };

                    var newPos = currentPos + walkerDirection[j];
                    var forceChange = newPos.x < 0 || newPos.x > mapSize -1 || newPos.y < 0 || newPos.y > mapSize -1;
                    
                    for (int k = availableDirections.Count -1; k >= 0; k--)
                    {
                        var testDirection = availableDirections[k];
                        var testPos = currentPos + testDirection;
                        if (testPos.x < 0 || testPos.x > mapSize -1 || testPos.y < 0 || testPos.y > mapSize -1)
                            availableDirections.RemoveAt(k);
                    }
                    
                    if (forceChange || Random.Range(0, 3) == 0)
                    {
                        walkerDirection[j] = availableDirections[Random.Range(0, availableDirections.Count - 1)];
                    }

                    currentPos += walkerDirection[j];

                    walkerPosition[j] = currentPos;
                }
            }
        }
    }
}