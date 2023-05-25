using System;
using Sources.StringController;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Views
{
    public class PlayerView : MonoBehaviour
    {
        [Header(HeaderNames.Objects)]
        [SerializeField] private Button _playButton;
        [SerializeField] private Slider _progressBar;
        
        public event Action Click;

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
            
            _playButton.onClick.AddListener(OnClick);
        }

        private void OnDisable() => _playButton.onClick.RemoveListener(OnClick);

        public void SetProgressBarValue(float currentProgress) => _progressBar.value = currentProgress;
        public void SetMaxSliderValue(Vector3 startPos, Vector3 endPos) => _progressBar.maxValue = Vector2.Distance(startPos, endPos);
        public void EnableStartButton() => _playButton.gameObject.SetActive(true);
 
        private void Validate()
        {
            if (_playButton == null)
                throw new InvalidOperationException();
        }

        private void OnClick()
        {
            if (_playButton.isActiveAndEnabled)
            {
                DisableStartButton();
                Click?.Invoke();
            }
        }

        private void DisableStartButton() => _playButton.gameObject.SetActive(false);
    }
}
