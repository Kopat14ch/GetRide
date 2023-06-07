using System.Collections.Generic;
using UnityEngine;

namespace Sources.EnemyScripts
{
    public class EnemyList : MonoBehaviour
    {
        private readonly List<Enemy> _enemies = new List<Enemy>();

        public IReadOnlyList<Enemy> Enemies => _enemies.GetRange(0, _enemies.Count);

        public void AddEnemy(Enemy enemy)
        {
            if (_enemies.Contains(enemy) == false)
                _enemies.Add(enemy);
        }

        public void MoveLastPositionAll()
        {
            foreach (var enemy in _enemies)
                if (enemy.Movement.isActiveAndEnabled)
                    enemy.Movement.MoveTo(enemy.LastPosition, enemy);
        }
    }
}