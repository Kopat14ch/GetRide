using System.Collections.Generic;
using Sources.EnemyScripts;

namespace Sources.Boosters
{
    public class MagicTrafficLight : Booster
    {
        private readonly List<EnemyTransformation> _enemies = new List<EnemyTransformation>();
        
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
        
        public void SetEnemies(IReadOnlyList<EnemyTransformation> enemies) => _enemies.AddRange(enemies);
    }
}