using System.Collections.Generic;
using Sources.EnemyScripts;
using Sources.Level.Roads;
using UnityEngine;

namespace Sources.Spawners
{
    
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Enemy _enemyPrefab;

        public void Spawn(IReadOnlyList<Road> roads, CenterRoad centerRoad)
        {
            Debug.Log(roads.Count);
            foreach (var road in roads)
            {
                Instantiate(_enemyPrefab, road.LevelPoint.GetPosition, Quaternion.Euler(centerRoad.Position), road.LevelPoint.transform);
            }
        }
    }
}