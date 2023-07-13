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
        [SerializeField] private EnemyTransformation _enemyTransformationPrefab;
        [SerializeField] private EnemyList _enemyList;

        private int _seed;
        private float _minTimeToEndPoint;
        private float _maxTimeToEndPoint;
        private float _rotateValue = -90f;

        public IEnumerable<EnemyTransformation> Enemies => _enemyList.Enemies;

        public void Initialize(int seed) => _seed = seed;

        public void Spawn(IReadOnlyList<Road> roads, PlayerView playerView)
        {
            int seed = System.DateTime.Now.Millisecond;
            
            Debug.Log(seed);
            Random.InitState(seed);
            int step = 2;
            int nextIndex = 1;
            
            _maxTimeToEndPoint = roads.Count + step;
            _minTimeToEndPoint = 2f;

            if (_maxTimeToEndPoint <= _minTimeToEndPoint)
                _maxTimeToEndPoint = _minTimeToEndPoint + 1;

            for (int i = 0; i < roads.Count; i++)
            {
                int randomValue = Random.Range(i, i + step);
                int roadIndex;

                EnemyTransformation enemyTransformation = Instantiate(_enemyTransformationPrefab, roads[randomValue].Point.GetPosition, Quaternion.identity, roads[randomValue].Point.transform);

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

                enemyTransformation.Init(roads[roadIndex].Point, playerView, roads[randomValue], Random.Range(_minTimeToEndPoint, _maxTimeToEndPoint));
                enemyTransformation.Rotate(_rotateValue);
                enemyTransformation.Collider.CollisionACharacter += _enemyList.MoveLastPositionAll;
                
                _enemyList.AddEnemy(enemyTransformation);

                if (_maxTimeToEndPoint - 1 <= _minTimeToEndPoint)
                    _maxTimeToEndPoint = _minTimeToEndPoint + 1;
                else
                    _maxTimeToEndPoint--;
                
                i++;
            }
        }
    }
}