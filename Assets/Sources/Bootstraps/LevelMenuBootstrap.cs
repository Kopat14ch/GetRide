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
            yield return YandexGamesSdk.Initialize(Init);
        }

        private void Init()
        {
            _saver.Initialize();
            _levelButtons.Initialize();
            _musicController.Initialize();
            _settings.Initialize(this);
        }

        public int GetLevelNumber() => _levelButtons.LevelNumber;
        public float GetAudioSourceValue() => _musicController.GetVolume();
        public void SetAudioVolume(float value) => _musicController.SetVolume(value);
    }
}