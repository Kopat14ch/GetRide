using System.Collections.Generic;
using UnityEngine;

namespace Sources.EnemyScripts
{
    public class EnemyList : MonoBehaviour
    {
        private readonly List<EnemyTransformation> _enemies = new List<EnemyTransformation>();

        public IReadOnlyList<EnemyTransformation> Enemies => _enemies.GetRange(0, _enemies.Count);

        public void AddEnemy(EnemyTransformation enemyTransformation)
        {
            if (_enemies.Contains(enemyTransformation) == false)
                _enemies.Add(enemyTransformation);
        }

        public void MoveLastPositionAll()
        {
            foreach (var enemy in _enemies)
                if (enemy.Movement.isActiveAndEnabled)
                    enemy.Movement.MoveTo(enemy.LastPosition, enemy);
        }
    }
}