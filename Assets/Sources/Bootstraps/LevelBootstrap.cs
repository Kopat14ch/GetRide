using IJunior.TypedScenes;
using Sources.Boosters;
using Sources.Level;
using Sources.Spawners;
using Sources.Views;
using UnityEngine;

namespace Sources.Bootstraps
{
    public class LevelBootstrap : MonoBehaviour , ISceneLoadHandler<int>
    {
        [SerializeField] private BoostersList _boostersList;
        [SerializeField] private PlayerView _playerView;
        [SerializeField] private LevelGenerator _levelGenerator;
        [SerializeField] private EnemiesSpawner _enemiesSpawner;
        
        private int _roadCount;
        private int _seed;

        private void Awake()
        {
            _enemiesSpawner.Initialize(_seed);
            _levelGenerator.SetRoadCount(_roadCount);

            foreach (var boosterView in _boostersList.BoosterViews)
                boosterView.Initialize();

            foreach (var boosterSetup in _boostersList.BoosterSetups)
                boosterSetup.Initialize(_playerView);
        }

        public void OnSceneLoaded(int numberValue)
        {
            _roadCount = LevelConfig.GetRoads(numberValue);
            _seed = LevelConfig.GetSeed(numberValue);
        }
    }
}
