using Agava.YandexGames;
using IJunior.TypedScenes;
using Lean.Localization;
using Sources.Bootstraps;
using Sources.Common;
using Sources.LevelMenu;
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
        private const int ShowsToAdVideo = 4;

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
            if (s_showsCount == ShowsToAdPicture)
            {
                InterstitialAd.Show(onOpenCallback: AdController.OnOpenAd, onOfflineCallback: AdController.OnCloseAd);
                
                s_showsCount++;
            }
            else if (s_showsCount == ShowsToAdVideo)
            {
                VideoAd.Show(onOpenCallback: AdController.OnOpenAd, onRewardedCallback: () => s_showsCount = 0, onCloseCallback: AdController.OnCloseAd);
            }
            else
            {
                s_showsCount++;
            }
            
            int score = isExcess ? ScoreWithExcess : ScoreWithoutExcess;

            gameObject.SetActive(true);
            
            _movementsCountText.text = $"{LeanLocalization.GetTranslationText(EnemiesMovementCount)}{movementsCount}{SeparationElement}{maxMovements}";
            _scoreText.text = $"{LeanLocalization.GetTranslationText(Score)}{score}";
            
            LevelConfig.Instance.SetScore(score, _levelNumber, ScoreWithExcess);
            
            Saver.Instance.SaveShows(s_showsCount);
            
            Time.timeScale = 0f;
        }

        private void SetNextLevel()
        {
            if (_levelNumber + 1 <= LevelButtons.MaxNumber)
                AsyncLoadScene.Instance.Load(IJunior.TypedScenes.Level.LoadAsync(_levelNumber));
        }

        private void SetMenuScene()
        {
            Time.timeScale = 1f;
            
            AsyncLoadScene.Instance.Load(LevelsMenu.LoadAsync());
        }
    }
}