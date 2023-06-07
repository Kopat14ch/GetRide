using System.Collections.Generic;
using Sources.EnemyScripts;
using Sources.Level.Roads;
using Sources.StringController;
using Sources.Views;
using UnityEngine;

namespace Sources.Spawners
{
    public class EnemiesSpawner : MonoBehaviour
    {
        [Header(HeaderNames.Objects)]
        [SerializeField] private Enemy _enemyPrefab;
        [SerializeField] private EnemyList _enemyList;

        private float _minTimeToEndPoint;
        private float _maxTimeToEndPoint;
        private float _rotateValue = -90f;

        public IReadOnlyList<Enemy> Enemies => _enemyList.Enemies;

        public void Spawn(IReadOnlyList<Road> roads, PlayerView playerView)
        {
            int step = 2;
            int nextIndex = 1;
            _maxTimeToEndPoint = roads.Count;
            _minTimeToEndPoint = 2.5f;

            if (_maxTimeToEndPoint <= _minTimeToEndPoint)
                _maxTimeToEndPoint = _minTimeToEndPoint + 1;

            for (int i = 0; i < roads.Count; i++)
            {
                int randomValue = Random.Range(i, i + step);
                int roadIndex;

                Enemy enemy = Instantiate(_enemyPrefab, roads[randomValue].Point.GetPosition, Quaternion.identity, roads[randomValue].Point.transform);

                if (randomValue % 2 == 0)
                {
                    roadIndex = randomValue + nextIndex;

                    if (_rotateValue < 0)
                        _rotateValue = -_rotateValue;
                }
                else
                {
                    roadIndex = randomValue - nextIndex;
                    
                    if (_rotateValue > 0)
                        _rotateValue = -_rotateValue;
                }

                enemy.Init(roads[roadIndex].Point, playerView, roads[randomValue], Random.Range(_minTimeToEndPoint, _maxTimeToEndPoint));
                enemy.Rotate(_rotateValue);
                enemy.Collider.CollisionACharacter += _enemyList.MoveLastPositionAll;
                
                _enemyList.AddEnemy(enemy);

                if (_maxTimeToEndPoint - 1 <= _minTimeToEndPoint)
                    _maxTimeToEndPoint = _minTimeToEndPoint + 1;
                else
                    _maxTimeToEndPoint--;
                
                i++;
            }
        }
    }
}