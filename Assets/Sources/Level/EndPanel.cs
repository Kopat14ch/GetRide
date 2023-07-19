using System;
using Agava.YandexGames;
using IJunior.TypedScenes;
using Lean.Localization;
using Sources.Common;
using Sources.Leaderboard;
using Sources.LevelMenu;
using Sources.Settings;
using Sources.StringController;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Level
{
    public class EndPanel : MonoBehaviour
    {
        [Header(HeaderNames.Objects)]
        [SerializeField] private TextMeshProUGUI _movementsCountText;
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private Button _menuButton;
        [SerializeField] private Button _nextLevelButton;
        
        private const string EnemiesMovementCount = nameof(EnemiesMovementCount);
        private const string Score = nameof(Score);
        private const string SeparationElement = "/";
        private const int ScoreWithoutExcess = 750;
        private const int ScoreWithExcess = 450;
        private const int ShowsToAdPicture = 2;

        private int _levelNumber;

        private static int s_showsCount;

        private void OnEnable()
        {
            _menuButton.onClick.AddListener(SetMenuScene);
            _nextLevelButton.onClick.AddListener(SetNextLevel);
        }

        private void OnDisable()
        {
            _menuButton.onClick.AddListener(SetMenuScene);
            _nextLevelButton.onClick.RemoveListener(SetNextLevel);
        }
        
        public void Initialize(int levelNumber)
        {
            s_showsCount = Saver.Instance.SaveData.ShowsCount;
            _levelNumber = levelNumber;

            gameObject.SetActive(false);
        }

        public void Show(int maxMovements ,int movementsCount, bool isExcess)
        {
            LeaderboardUI.Instance.SetCanOpen(false);
            
            SoundController.Instance.PlayEndPanel();

            if (LevelConfig.Instance.GetLevelsCount() >= _levelNumber + 1)
                LevelConfig.Instance.UnLock(_levelNumber + 1);

            if (s_showsCount == ShowsToAdPicture)
            {
                InterstitialAd.Show(onOpenCallback: AdController.OnOpenAd, onCloseCallback: (bool value) => AdController.OnCloseAd());
                s_showsCount = 0;
            }

            int score = isExcess ? ScoreWithExcess : ScoreWithoutExcess;

            gameObject.SetActive(true);
            
            _movementsCountText.text = $"{LeanLocalization.GetTranslationText(EnemiesMovementCount)}{movementsCount}{SeparationElement}{maxMovements}";
            _scoreText.text = $"{LeanLocalization.GetTranslationText(Score)}{score}";
            s_showsCount++;
            
            LevelConfig.Instance.SetScore(score, _levelNumber, ScoreWithExcess);
            
            Saver.Instance.SaveShows(s_showsCount);
            Saver.Instance.EndTraining();

            Time.timeScale = 0f;
        }

        private void SetNextLevel()
        {
            if (_levelNumber + 1 <= LevelButtons.MaxNumber)
                AsyncLoadScene.Instance.Load(IJunior.TypedScenes.Level.LoadAsync(_levelNumber + 1));
            
        }

        private void SetMenuScene()
        {
            Time.timeScale = 1f;
            
            AsyncLoadScene.Instance.Load(LevelsMenu.LoadAsync());
        }
    }
}