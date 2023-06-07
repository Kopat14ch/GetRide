using System.Collections.Generic;
using Sources.EnemyScripts;
using UnityEngine;

namespace Sources.Boosters
{
    public class BoostersList : MonoBehaviour
    {
        private MagicTrafficLight _magicTrafficLight;

        private void Awake()
        {
            _magicTrafficLight = GetComponentInChildren<MagicTrafficLight>();
        }

        public void MagicSetEnemies(IReadOnlyList<Enemy> enemies)
        {
            _magicTrafficLight.SetEnemies(enemies);
        }
    }
}