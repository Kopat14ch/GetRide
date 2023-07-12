using Sources.Leaderboard;
using Sources.LevelMenu;
using Sources.Settings;
using Sources.StringController;
using UnityEngine;

namespace Sources.Bootstraps
{
    public class LevelMenuBootstrap : MonoBehaviour
    {
        [Header(HeaderNames.Objects)]
        [SerializeField] private LevelButtons _levelButtons;
        [SerializeField] private SettingsMenu _settings;
        [SerializeField] private LeaderboardUI _leaderboardUI;
        
        private void Awake()
        {
            _settings.Initialize();
            _levelButtons.Initialize();
            _leaderboardUI.Initialize();

            SettingsMenu.Instance.DisableMenuButton();
            SettingsMenu.Instance.DisablePanel();
        }
    }
}