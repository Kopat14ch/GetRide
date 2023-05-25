using System.Collections.Generic;
using UnityEngine;

namespace Sources.EnemyScripts
{
    public class EnemyList : MonoBehaviour
    {
        private readonly List<Enemy> _enemies = new List<Enemy>();

        public void AddEnemy(Enemy enemy)
        {
            if (_enemies.Contains(enemy) == false)
                _enemies.Add(enemy);
        }

        public void MoveLastPositionAll()
        {
            foreach (var enemy in _enemies)
                enemy.Movement.MoveTo(enemy.LastPosition, enemy);
        }
    }
}