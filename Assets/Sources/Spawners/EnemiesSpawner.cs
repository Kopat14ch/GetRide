using System.Collections.Generic;
using Sources.EnemyScripts;
using Sources.Level.Roads;
using Sources.Views;
using UnityEngine;

namespace Sources.Spawners
{
    public class EnemiesSpawner : MonoBehaviour
    {
        [SerializeField] private Enemy _enemyPrefab;

        public void Spawn(IReadOnlyList<Road> roads, CenterRoad centerRoad, PlayerView playerView)
        {
            Enemy enemy;
            int step = 2;

            for (int i = 0; i < roads.Count; i++)
            {
                int tempValue;
                int randomValue = Random.Range(i, i + step);

                enemy = Instantiate(_enemyPrefab, roads[randomValue].Point.GetPosition, Quaternion.Euler(centerRoad.Position), roads[randomValue].Point.transform);
                tempValue = randomValue % 2 == 0 ? randomValue + 1 : randomValue - 1;
                
                enemy.Init(roads[tempValue].Point, playerView);
                i++;
            }
        }


    }
}