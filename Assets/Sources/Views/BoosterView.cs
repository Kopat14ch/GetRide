using System;
using Agava.YandexGames;
using Sources.Boosters;
using Sources.Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Views
{
    [RequireComponent(typeof(Booster))]
    public class BoosterView : MonoBehaviour
    {
        [SerializeField] private PlayerView _playerView;
        
        private VideoImage _videoImage;
        private Booster _booster;
        private Button _button;
        private TextMeshProUGUI _countText;

        public event Action<Booster> BoosterActivated;
        public event Action BoosterActivatedTraining;
        public event Action VideoAwardReceived;

        public void Initialize()
        {
            _booster = GetComponent<Booster>();
            _button = GetComponentInChildren<Button>();
            _countText = GetComponentInChildren<TextMeshProUGUI>();
            _videoImage = GetComponentInChildren<VideoImage>();
            
            TryActiveVideoImage();

            _countText.text = Saver.Instance.GetSavedBoosterCount(_booster).ToString();
        }

        private void OnEnable()
        {
            try
            {
                Validate();
            }
            catch (Exception e)
            {
                enabled = false;
                throw e;
            }
            
            _button.onClick.AddListener(OnButtonClicked);
        }

        private void OnDisable() => _button.onClick.RemoveListener(OnButtonClicked);
        
        public void SetCount(int count)
        {
            TryActiveVideoImage();
            _countText.text = count.ToString();
        }

        public void ShowVideo() => OnShowVideo();

        
        private void Validate()
        {
            if (_booster == null || _button == null)
                throw new NullReferenceException();
        }

        private void OnShowVideo() => VideoAd.Show(onOpenCallback: AdController.OnOpenAd, onRewardedCallback: () => VideoAwardReceived?.Invoke(), onCloseCallback: AdController.OnCloseAd);

        private void TryActiveVideoImage()
        {
            if (Saver.Instance.GetSavedBoosterCount(_booster) == 0)
            {
                _videoImage.Enable();
                _button.GetComponent<Image>().color = _button.colors.disabledColor;
            }
            else
            {
                _button.GetComponent<Image>().color = _button.colors.normalColor;
                _videoImage.Disable();
            }
        }

        private void OnButtonClicked()
        {
            if (Saver.Instance.SaveData.IsTrained)
            {
                if (_playerView.CanPlay)
                {
                    BoosterActivated?.Invoke(_booster);
                }
            }
            else
            {
                if (_playerView.CanActivateBoosterInTraining)
                {
                    BoosterActivated?.Invoke(_booster);
                    BoosterActivatedTraining?.Invoke();
                }
            }
        }
    }
}