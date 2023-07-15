using Agava.WebUtility;
using Sources.Common;
using Sources.Leaderboard;
using Sources.Level;
using Sources.Music;
using Sources.StringController;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Settings
{
    public class SettingsMenu : MonoBehaviour
    {
        [Header(HeaderNames.Objects)]
        [SerializeField] private Slider _audioSlider;
        [SerializeField] private Button _toggleMenu;
        [SerializeField] private Sprite _enableSprite;
        [SerializeField] private Sprite _disableSpite;
        [SerializeField] private ExitMenuButton _exitMenuButton;
        [SerializeField] private MusicController _musicController;
        [SerializeField] private Toggle _toggleMusic;

        public static SettingsMenu Instance { get; private set; }
        public bool IsToggleMusicEnabled { get; private set; }

        private Panel _panel;

        public void Initialize()
        {
            _musicController.Initialize();
            
            if (Instance == null)
            {
                transform.parent = null;
                DontDestroyOnLoad(gameObject);
                Instance = this;
                
                _panel = GetComponentInChildren<Panel>();
                _toggleMenu.GetComponent<Image>().sprite = _enableSprite;

                _audioSlider.value = _musicController.GetVolume();

                IsToggleMusicEnabled = _toggleMusic.isOn;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void OnEnable()
        {
            _toggleMenu.onClick.AddListener(Toggle);
            _audioSlider.onValueChanged.AddListener(OnAudioSliderChangeValue);
            _toggleMusic.onValueChanged.AddListener(OnToggleMusicValueChanged);
            WebApplication.InBackgroundChangeEvent += OnBackgroundChangeEvent;
        }

        private void OnDisable()
        {
            _toggleMenu.onClick.RemoveListener(Toggle);
            _audioSlider.onValueChanged.RemoveListener(OnAudioSliderChangeValue);
            _toggleMusic.onValueChanged.RemoveListener(OnToggleMusicValueChanged);
            WebApplication.InBackgroundChangeEvent -= OnBackgroundChangeEvent;
        }

        public void DisablePanel()
        {
            _toggleMenu.GetComponent<Image>().sprite = _enableSprite;
            _panel.Disable();
        }
        
        public void EnableMenuButton() => _exitMenuButton.Enable();
        public void DisableMenuButton() => _exitMenuButton.Disable();

        public void DisableMusic() => _musicController.DisableMusic();
        public void EnableMusic() => _musicController.EnableMusic();

        private void OnToggleMusicValueChanged(bool value)
        {
            if (value)
            {
                EnableMusic();
                foreach (var gameObjectTemp in _toggleMusic.GetComponentsInChildren<GameObject>())
                    gameObjectTemp.SetActive(true);
            }
            else
            {
                foreach (var gameObjectTemp in _toggleMusic.GetComponentsInChildren<GameObject>())
                    gameObjectTemp.SetActive(false);

                DisableMusic();
            }
            
            IsToggleMusicEnabled = value;
        }

        private void EnablePanel()
        {
            _toggleMenu.GetComponent<Image>().sprite = _disableSpite;
            _panel.Enable();
        }

        private void Toggle()
        {
            if (_panel.isActiveAndEnabled)
            {
                DisablePanel();

                Time.timeScale = 1f;
            }
            else
            {
                EnablePanel();
                LeaderboardUI.Instance.Disable();

                Time.timeScale = 0f;
            }
        }

        private void OnBackgroundChangeEvent(bool value)
        {
            if (IsToggleMusicEnabled == false)
                return;

            if (value)
                DisableMusic();
            else
                EnableMusic();
        }

        private void OnAudioSliderChangeValue(float value) => _musicController.SetVolume(value);
    }
}