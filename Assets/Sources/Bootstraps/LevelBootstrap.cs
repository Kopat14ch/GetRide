using Agava.YandexGames;
using IJunior.TypedScenes;
using Sources.Boosters;
using Sources.Common;
using Sources.Leaderboard;
using Sources.Level;
using Sources.Settings;
using Sources.Spawners;
using Sources.StringController;
using Sources.Training;
using Sources.Views;
using UnityEngine;

namespace Sources.Bootstraps
{
    public class LevelBootstrap : MonoBehaviour, ISceneLoadHandler<int>
    {
        [Header(HeaderNames.Objects)]
        [SerializeField] private EndPanel _endPanel;
        [SerializeField] private BoostersList _boostersList;
        [SerializeField] private PlayerView _playerView;
        [SerializeField] private LevelGenerator _levelGenerator;
        [SerializeField] private EnemiesSpawner _enemiesSpawner;
        [SerializeField] private EnemiesInvolvedSlider _enemiesInvolvedSlider;
        [SerializeField] private TrainingUI _trainingUI;

        private int _roadCount;
        private int _seed;
        private int _maxEnemiesDragging;
        private int _levelNumber;

        private void Awake()
        {
            if (Saver.Instance.SaveData.IsTrained)
                InterstitialAd.Show(onOpenCallback: AdController.OnOpenAd, onCloseCallback: (value) => AdController.OnCloseAd());

            LeaderboardUI.Instance.SetCanOpen(true);
            LeaderboardUI.Instance.SetTrainingUI(_trainingUI);
            SettingsMenu.Instance.SetTrainingUI(_trainingUI);

            Time.timeScale = 1f;
            
            _playerView.Initialize(_maxEnemiesDragging);
            _boostersList.Initialize();
            
            _enemiesSpawner.Initialize(_seed);
            _levelGenerator.SetRoadCount(_roadCount);

            foreach (var boosterView in _boostersList.BoosterViews)
                boosterView.Initialize();
            
            foreach (var boosterSetup in _boostersList.BoosterSetups)
                boosterSetup.Initialize();

            _enemiesInvolvedSlider.Initialize(_maxEnemiesDragging);

            _endPanel.Initialize(_levelNumber);
            
            SettingsMenu.Instance.EnableMenuButton();
        }
        
        public void OnSceneLoaded(int levelNumber)
        {
            _levelNumber = levelNumber;

            _roadCount = LevelConfig.Instance.GetRoads(levelNumber);
            _seed = LevelConfig.Instance.GetSeed(levelNumber);
            _maxEnemiesDragging = LevelConfig.Instance.GetMaxEnemiesDragging(levelNumber);
        }
    }
}
