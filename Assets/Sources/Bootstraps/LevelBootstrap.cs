using IJunior.TypedScenes;
using Sources.Boosters;
using Sources.Level;
using Sources.Settings;
using Sources.Spawners;
using Sources.Views;
using UnityEngine;

namespace Sources.Bootstraps
{
    public class LevelBootstrap : MonoBehaviour, ISceneLoadHandler<LevelMenuBootstrap>
    {
        [SerializeField] private BoostersList _boostersList;
        [SerializeField] private PlayerView _playerView;
        [SerializeField] private LevelGenerator _levelGenerator;
        [SerializeField] private EnemiesSpawner _enemiesSpawner;
        [SerializeField] private SettingsMenu _settings;

        private LevelMenuBootstrap _levelMenuBootstrap;
        private int _roadCount;
        private int _seed;

        private void Awake()
        {
            _enemiesSpawner.Initialize(_seed);
            _levelGenerator.SetRoadCount(_roadCount);
            _settings.Initialize(_levelMenuBootstrap);

            foreach (var boosterView in _boostersList.BoosterViews)
                boosterView.Initialize();
            
            foreach (var boosterSetup in _boostersList.BoosterSetups)
                boosterSetup.Initialize(_playerView);
            foreach (var boosterSetup in _boostersList.BoosterSetups)
                boosterSetup.Initialize(_playerView);
        }

        public void OnSceneLoaded(LevelMenuBootstrap levelMenuBootstrap)
        {
            _roadCount = LevelConfig.GetRoads(levelMenuBootstrap.GetLevelNumber());
            _seed = LevelConfig.GetSeed(levelMenuBootstrap.GetLevelNumber());
            _levelMenuBootstrap = levelMenuBootstrap;
        }
    }
}
