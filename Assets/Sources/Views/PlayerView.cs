using System;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Views
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private Button _playButton;

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

        private void OnDisable()
        {
            _playButton.onClick.RemoveListener(OnClick);
        }
        
        private void Validate()
        {
            if (_playButton == null)
                throw new InvalidOperationException();
        }

        private void OnClick() => Click?.Invoke();
    }
}
