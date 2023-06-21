using System.Collections.Generic;
using Sources.EnemyScripts;
using Sources.PlayerScripts;
using Sources.Setups;
using Sources.Views;
using UnityEngine;

namespace Sources.Boosters
{
    public class BoostersList : MonoBehaviour
    {
        [SerializeField] private List<BoosterView> _boosterViews;
        [SerializeField] private List<BoosterSetup> _boosterSetups;

        private MagicTrafficLight _magicTrafficLight;
        private MagicPotion _magicPotion;

        public IReadOnlyList<BoosterView> BoosterViews => _boosterViews.AsReadOnly();
        public IReadOnlyList<BoosterSetup> BoosterSetups => _boosterSetups.AsReadOnly();

        private void Awake()
        {
            _magicTrafficLight = GetComponentInChildren<MagicTrafficLight>();
            _magicPotion = GetComponentInChildren<MagicPotion>();
        }

        public void InitBoosters(IReadOnlyList<EnemyTransformation> enemies, Character character)
        {
            _magicTrafficLight.SetEnemies(enemies);
            _magicPotion.SetCharacter(character);
        }
    }
}