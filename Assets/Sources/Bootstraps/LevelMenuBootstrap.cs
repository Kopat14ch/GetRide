using System.Collections;
using Agava.YandexGames;
using Sources.Common;
using Sources.LevelMenu;
using Sources.Music;
using Sources.Settings;
using UnityEngine;

namespace Sources.Bootstraps
{
    public class LevelMenuBootstrap : MonoBehaviour
    {
        [SerializeField] private Saver _saver;
        [SerializeField] private LevelButtons _levelButtons;
        [SerializeField] private MusicController _musicController;
        [SerializeField] private SettingsMenu _settings;

        private void Awake()
        {
            YandexGamesSdk.CallbackLogging = true;
        }

        private IEnumerator Start()
        {
            if (YandexGamesSdk.IsInitialized)
                Init();
            else
                yield return YandexGamesSdk.Initialize(Init);
        }
        
        public float GetMusicVolume => _musicController.GetVolume();
        public int GetLevelNumber() => _levelButtons.LevelNumber;
        public void SetAudioVolume(float value) => _musicController.SetVolume(value);
        
        private void Init()
        {
            if (Saver.IsLoaded)
            {
                OnLoaded();
                
                return;
            }

            _saver.Initialize();
            _saver.Loaded += OnLoaded;
        }

        private void OnLoaded()
        {
            _levelButtons.Initialize();
            _musicController.Initialize();
            _settings.Initialize(this);
            
            if (Saver.IsLoaded == false)
            {
                _saver.Loaded -= OnLoaded;
            }
            
            SettingsMenu.Instance.DisableMenuButton();
            SettingsMenu.Instance.DisablePanel();
        }
    }
}