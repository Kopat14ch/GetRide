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

        private int _levelNumber;

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
            _levelNumber = levelNumber;

            gameObject.SetActive(false);
        }

        public void Show(int maxMovements ,int movementsCount, bool isExcess)
        {
            gameObject.SetActive(true);

            if (Saver.Instance.SaveData.IsTrained == false)
                Saver.Instance.SetEndTraining();

            SettingsMenu.Instance.DisableTimeScaleSet();
            LeaderboardUI.Instance.SetCanOpen(false);
            
            SoundController.Instance.PlayEndPanel();

            if (LevelConfig.Instance.GetLevelsCount() >= _levelNumber + 1)
                LevelConfig.Instance.UnLock(_levelNumber + 1);
            
            int score = isExcess ? ScoreWithExcess : ScoreWithoutExcess;

            _movementsCountText.text = $"{LeanLocalization.GetTranslationText(EnemiesMovementCount)}{movementsCount}{SeparationElement}{maxMovements}";
            _scoreText.text = $"{LeanLocalization.GetTranslationText(Score)}{score}";

            LevelConfig.Instance.SetScore(score, _levelNumber, ScoreWithExcess);

            Time.timeScale = 0f;
        }

        private void SetNextLevel()
        {
            if (_levelNumber + 1 > LevelButtons.MaxNumber)
                return;;
            
            Time.timeScale = 1f;

            AsyncLoadScene.Instance.Load(IJunior.TypedScenes.Level.LoadAsync(_levelNumber + 1));
        }

        private void SetMenuScene()
        {
            Time.timeScale = 1f;

            AsyncLoadScene.Instance.Load(LevelsMenu.LoadAsync());
        }
    }
}