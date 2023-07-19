using Sources.Leaderboard;
using Sources.Level;
using Sources.LevelMenu;
using Sources.Settings;
using Sources.StringController;
using Sources.Training;
using UnityEngine;

namespace Sources.Bootstraps
{
    public class LevelMenuBootstrap : MonoBehaviour
    {
        [Header(HeaderNames.Objects)]
        [SerializeField] private LevelButtons _levelButtons;
        [SerializeField] private SettingsMenu _settings;
        [SerializeField] private LeaderboardUI _leaderboardUI;
        [SerializeField] private LevelConfig _levelConfig;
        [SerializeField] private TrainingUI _trainingUI;

        private void Awake()
        {
            _levelConfig.Initialize();
            _levelButtons.Initialize();
            _settings.Initialize(_trainingUI);
            _settings.SetTrainingUI(_trainingUI);
            _leaderboardUI.Initialize();
            _leaderboardUI.SetTrainingUI(_trainingUI);
            
            LeaderboardUI.Instance.SetCanOpen(true);
            SettingsMenu.Instance.DisableMenuButton();
            SettingsMenu.Instance.DisablePanel();
        }
    }
}