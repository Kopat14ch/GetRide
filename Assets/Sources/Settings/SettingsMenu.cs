using Agava.WebUtility;
using Sources.Common;
using Sources.Leaderboard;
using Sources.Level;
using Sources.Music;
using Sources.StringController;
using Sources.Training;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Settings
{
    public class SettingsMenu : MonoBehaviour
    {
        [Header(HeaderNames.Objects)]
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Slider _soundSlider;
        [SerializeField] private Button _toggleMenu;
        [SerializeField] private Sprite _enableSprite;
        [SerializeField] private Sprite _disableSpite;
        [SerializeField] private ExitMenuButton _exitMenuButton;
        [SerializeField] private MusicController _musicController;
        [SerializeField] private SoundController _soundController;
        [SerializeField] private Toggle _toggleMusic;
        [SerializeField] private Toggle _toggleSound;

        private Panel _panel;
        private TrainingUI _trainingUI;
        private bool _canSetTimeScale;
        
        public static SettingsMenu Instance { get; private set; }
        public bool IsToggleMusicEnabled { get; private set; }

        public void Initialize(TrainingUI trainingUI = null)
        {
            _musicController.Initialize();
            _canSetTimeScale = true;
            
            if (Instance == null)
            {
                transform.parent = null;
                DontDestroyOnLoad(gameObject);
                Instance = this;
                _trainingUI = trainingUI;
                _panel = GetComponentInChildren<Panel>();
                _toggleMenu.GetComponent<Image>().sprite = _enableSprite;

                _musicSlider.value = _musicController.GetVolume();

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
            _musicSlider.onValueChanged.AddListener(OnMusicSliderChangeValue);
            _soundSlider.onValueChanged.AddListener(OnSoundSliderChangeValue);
            _toggleMusic.onValueChanged.AddListener(OnToggleMusicValueChanged);
            _toggleSound.onValueChanged.AddListener(OnToggleSoundValueChanged);
            WebApplication.InBackgroundChangeEvent += OnBackgroundChangeEvent;
        }

        private void OnDisable()
        {
            _toggleMenu.onClick.RemoveListener(Toggle);
            _musicSlider.onValueChanged.RemoveListener(OnMusicSliderChangeValue);
            _soundSlider.onValueChanged.RemoveListener(OnSoundSliderChangeValue);
            _toggleMusic.onValueChanged.RemoveListener(OnToggleMusicValueChanged);
            _toggleSound.onValueChanged.RemoveListener(OnToggleSoundValueChanged);
            WebApplication.InBackgroundChangeEvent -= OnBackgroundChangeEvent;
        }

        public void DisablePanel()
        {
            _toggleMenu.GetComponent<Image>().sprite = _enableSprite;
            _panel.Disable();
        }

        public void SetTrainingUI(TrainingUI trainingUI)
        {
            if (Saver.Instance.SaveData.IsTrained == false)
                _trainingUI = trainingUI;
        }
        
        public void EnableMenuButton() => _exitMenuButton.Enable();
        public void DisableMenuButton() => _exitMenuButton.Disable();
        public void DisableMusic() => _musicController.DisableMusic();
        public void EnableMusic() => _musicController.EnableMusic();
        public void PauseSound() => _soundController.Pause();
        public void UnPauseSound() => _soundController.UnPause();
        public void DisableTimeScaleSet() => _canSetTimeScale = false;

        private void OnToggleMusicValueChanged(bool value)
        {
            if (value)
            {
                EnableMusic();
                
                _toggleMusic.isOn = true;
            }
            else
            {
                _toggleMusic.isOn = false;

                DisableMusic();
            }
            
            IsToggleMusicEnabled = value;
        }

        private void OnToggleSoundValueChanged(bool value)
        {
            if (value)
            {
                EnableSound();

                _toggleSound.isOn = true;
            }
            else
            {
                DisableSound();

                _toggleSound.isOn = false;
            }
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
                UnPauseSound();
                
                if (Saver.Instance.SaveData.IsTrained == false && _trainingUI.IsDisabled == false)
                    _trainingUI.gameObject.SetActive(true);

                Time.timeScale = _canSetTimeScale ? 1f : 0f;
            }
            else
            {
                EnablePanel();
                PauseSound();
                
                if (Saver.Instance.SaveData.IsTrained == false && _trainingUI.IsDisabled == false)
                    _trainingUI.gameObject.SetActive(false);
                
                LeaderboardUI.Instance.Disable();

                if (_canSetTimeScale)
                    Time.timeScale = 0f;
            }
        }

        private void OnBackgroundChangeEvent(bool value)
        {
            if (IsToggleMusicEnabled == false || AdController.IsOpen)
                return;

            if (value)
            {
                DisableMusic();
                Time.timeScale = 0f;
            }
            else
            {
                EnableMusic();
                Time.timeScale = 1f;
            }
        }

        private void OnMusicSliderChangeValue (float value) => _musicController.SetVolume(value);
        private void OnSoundSliderChangeValue(float value) => _soundController.SetVolume(value);
        private void EnableSound() => _soundController.SetMute(false);
        private void DisableSound() => _soundController.SetMute(true);
    }
}