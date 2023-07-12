using System.Collections.Generic;
using System.Linq;
using Sources.EnemyScripts;
using Sources.PlayerScripts;
using Sources.Setups;
using Sources.Views;
using UnityEngine;

namespace Sources.Boosters
{
    public class BoostersList : MonoBehaviour
    {
        private List<BoosterView> _boosterViews;
        private List<BoosterSetup> _boosterSetups;

        private MagicTrafficLight _magicTrafficLight;
        private MagicPotion _magicPotion;

        public IReadOnlyList<BoosterView> BoosterViews => _boosterViews.AsReadOnly();
        public IReadOnlyList<BoosterSetup> BoosterSetups => _boosterSetups.AsReadOnly();

        public void Initialize()
        {
            _boosterViews = GetComponentsInChildren<BoosterView>().ToList();
            _boosterSetups = GetComponentsInChildren<BoosterSetup>().ToList();
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