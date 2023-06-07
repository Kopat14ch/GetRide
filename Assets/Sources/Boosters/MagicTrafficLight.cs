using System.Collections.Generic;
using Sources.EnemyScripts;

namespace Sources.Boosters
{
    public class MagicTrafficLight : Booster
    {
        private readonly List<Enemy> _enemies = new List<Enemy>();
        
        public override void Activate()
        {
            if (_enemies.Count <= 0)
                return;

            foreach (var enemy in _enemies)
            {
                Destroy(enemy);
                Destroy(enemy.Movement);
            }
        }
        
        public void SetEnemies(IReadOnlyList<Enemy> enemies) => _enemies.AddRange(enemies);
    }
}