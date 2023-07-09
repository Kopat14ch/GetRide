using Sources.Leaderboard;
using Sources.Level;
using Sources.LevelMenu;
using Sources.Music;
using Sources.Settings;
using UnityEngine;

namespace Sources.Bootstraps
{
    public class LevelMenuBootstrap : MonoBehaviour
    {
        [SerializeField] private LevelButtons _levelButtons;
        [SerializeField] private MusicController _musicController;
        [SerializeField] private SettingsMenu _settings;
        [SerializeField] private LevelConfig _levelConfig;
        [SerializeField] private LeaderboardUI _leaderboardUI;
        
        public float GetMusicVolume => _musicController.GetVolume();
        public int GetLevelNumber() => _levelButtons.LevelNumber;
        public void SetAudioVolume(float value) => _musicController.SetVolume(value);
        
        private void Awake()
        {
            _levelConfig.Initialize();
            _musicController.Initialize();
            _settings.Initialize(this);
            _levelButtons.Initialize();
            _leaderboardUI.Initialize();

            SettingsMenu.Instance.DisableMenuButton();
            SettingsMenu.Instance.DisablePanel();
        }
    }
}