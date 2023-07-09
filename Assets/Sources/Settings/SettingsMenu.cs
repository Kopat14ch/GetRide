using Sources.Bootstraps;
using Sources.Common;
using Sources.Level;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Settings
{
    public class SettingsMenu : MonoBehaviour
    {
        [SerializeField] private Slider _audioSlider;
        [SerializeField] private Button _toggle;
        [SerializeField] private Sprite _enableSprite;
        [SerializeField] private Sprite _disableSpite;
        [SerializeField] private ExitMenuButton _exitMenuButton;

        public static SettingsMenu Instance { get; private set; }

        private Panel _panel;
        private LevelMenuBootstrap _levelMenuBootstrap;
        
        public void Initialize(LevelMenuBootstrap levelMenuBootstrap)
        {
            if (Instance == null)
            {
                transform.parent = null;
                DontDestroyOnLoad(gameObject);
                Instance = this;
                
                _panel = GetComponentInChildren<Panel>();
                _toggle.GetComponent<Image>().sprite = _enableSprite;
                _levelMenuBootstrap = levelMenuBootstrap;

                _audioSlider.value = levelMenuBootstrap.GetMusicVolume;

                DisablePanel();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void OnEnable()
        {
            _toggle.onClick.AddListener(Toggle);
            _audioSlider.onValueChanged.AddListener(OnAudioSliderChangeValue);
        }

        private void OnDisable()
        {
            _toggle.onClick.RemoveListener(Toggle);
            _audioSlider.onValueChanged.RemoveListener(OnAudioSliderChangeValue);
        }

        public void EnableMenuButton() => _exitMenuButton.Enable();
        public void DisableMenuButton() => _exitMenuButton.Disable();

        public void DisablePanel()
        {
            _toggle.GetComponent<Image>().sprite = _enableSprite;
            _panel.Disable();
        }

        private void EnablePanel()
        {
            _toggle.GetComponent<Image>().sprite = _disableSpite;
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

                Time.timeScale = 0f;
            }
        }

        private void OnAudioSliderChangeValue(float value) => _levelMenuBootstrap.SetAudioVolume(value);
    }
}