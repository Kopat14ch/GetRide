using Sources.Bootstraps;
using Sources.Common;
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
        
        private Panel _panel;
        private LevelMenuBootstrap _levelMenuBootstrap;

        public void Initialize(LevelMenuBootstrap levelMenuBootstrap)
        {
            _panel = GetComponentInChildren<Panel>();

            _panel.Disable();
            _toggle.GetComponent<Image>().sprite = _enableSprite;
            _levelMenuBootstrap = levelMenuBootstrap;

            if (Saver.Instance.SaveData.CanMusicChanged)
            {
                _audioSlider.value = Saver.Instance.SaveData.MusicVolumeValue;
                _levelMenuBootstrap.SetAudioVolume(Saver.Instance.SaveData.MusicVolumeValue);
            }
            else
            {
                _audioSlider.value = levelMenuBootstrap.GetAudioSourceValue();
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

        private void Toggle()
        {
            if (_panel.isActiveAndEnabled)
            {
                _toggle.GetComponent<Image>().sprite = _enableSprite;
                _panel.Disable();
                Time.timeScale = 1f;
            }
            else
            {
                _toggle.GetComponent<Image>().sprite = _disableSpite;
                _panel.Enable();
                Time.timeScale = 0f;
            }
        }

        private void OnAudioSliderChangeValue(float value) => _levelMenuBootstrap.SetAudioVolume(value);
    }
}