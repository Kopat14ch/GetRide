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
            foreach (var boosterSetup in _boostersList.BoosterSetups)
                boosterSetup.Initialize(_playerView);
            
            SettingsMenu.Instance.EnableMenuButton();
        }

        public void OnSceneLoaded(LevelMenuBootstrap levelMenuBootstrap)
        {
            _roadCount = LevelConfig.GetRoads(levelMenuBootstrap.GetLevelNumber());
            _seed = LevelConfig.GetSeed(levelMenuBootstrap.GetLevelNumber());
        }
    }
}
