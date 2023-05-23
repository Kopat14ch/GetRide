﻿using System.Collections.Generic;
using Sources.EnemyScripts;
using Sources.Level.Roads;
using Sources.Views;
using UnityEngine;

namespace Sources.Spawners
{
    public class EnemiesSpawner : MonoBehaviour
    {
        [SerializeField] private Enemy _enemyPrefab;

        private float _minTimeToEndPoint;
        private float _maxTimeToEndPoint;
        private float _rotateY = -90f;

        public void Spawn(IReadOnlyList<Road> roads, PlayerView playerView)
        {
            int step = 2;
            _minTimeToEndPoint = 2f;
            _maxTimeToEndPoint = 5f;
            

            for (int i = 0; i < roads.Count; i++)
            {
                int randomValue = Random.Range(i, i + step);
                int tempValue;

                var enemy = Instantiate(_enemyPrefab, roads[randomValue].Point.GetPosition, Quaternion.identity, roads[randomValue].Point.transform);

                if (randomValue % 2 == 0)
                {
                    tempValue = randomValue + 1;

                    if (_rotateY < 0)
                        _rotateY *= -1;
                }
                else
                {
                    tempValue = randomValue - 1;
                    
                    if (_rotateY > 0)
                        _rotateY *= -1;
                }
                
                enemy.Init(roads[tempValue].Point, playerView, roads[randomValue], Random.Range(_minTimeToEndPoint, _maxTimeToEndPoint));
                enemy.transform.Rotate(0,_rotateY,0);

                _minTimeToEndPoint++;
                _maxTimeToEndPoint++;
                i++;
            }
        }
    }
}