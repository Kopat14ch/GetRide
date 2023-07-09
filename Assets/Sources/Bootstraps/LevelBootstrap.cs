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
        [SerializeField] private EndPanel _endPanel;
        [SerializeField] private BoostersList _boostersList;
        [SerializeField] private PlayerView _playerView;
        [SerializeField] private LevelGenerator _levelGenerator;
        [SerializeField] private EnemiesSpawner _enemiesSpawner;
        [SerializeField] private EnemiesInvolvedSlider _enemiesInvolvedSlider;

        private int _roadCount;
        private int _seed;
        private int _maxEnemiesDragging;
        private int _levelNumber;
        private LevelMenuBootstrap _levelMenuBootstrap;

        private void Awake()
        {
            Time.timeScale = 1f;
            
            _playerView.Initialize(_maxEnemiesDragging, _levelNumber);
            
            _enemiesSpawner.Initialize(_seed);
            _levelGenerator.SetRoadCount(_roadCount);
            
            foreach (var boosterView in _boostersList.BoosterViews)
                boosterView.Initialize();
            
            foreach (var boosterSetup in _boostersList.BoosterSetups)
                boosterSetup.Initialize(_playerView);
            foreach (var boosterSetup in _boostersList.BoosterSetups)
                boosterSetup.Initialize(_playerView);
            
            _enemiesInvolvedSlider.Initialize(_maxEnemiesDragging);
            
            SettingsMenu.Instance.EnableMenuButton();

            _endPanel.Initialize(_levelMenuBootstrap);
        }

        public void OnSceneLoaded(LevelMenuBootstrap levelMenuBootstrap)
        {
            _levelNumber = levelMenuBootstrap.GetLevelNumber();
            _levelMenuBootstrap = levelMenuBootstrap;
            
            _roadCount = LevelConfig.Instance.GetRoads(levelMenuBootstrap.GetLevelNumber());
            _seed = LevelConfig.Instance.GetSeed(levelMenuBootstrap.GetLevelNumber());
            _maxEnemiesDragging = LevelConfig.Instance.GetMaxEnemiesDragging(levelMenuBootstrap.GetLevelNumber());
        }
    }
}
