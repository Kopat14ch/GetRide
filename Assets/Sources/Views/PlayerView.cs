using System;
using Sources.EnemyScripts;
using Sources.StringController;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Views
{
    public class PlayerView : MonoBehaviour
    {
        [Header(HeaderNames.Objects)]
        [SerializeField] private Slider _progressBar;

        private PlayerInput _playerInput;
        private Camera _camera;

        public event Action Click;

        private bool _canPlay; 

        private void Awake()
        {
            _playerInput = new PlayerInput();
            _camera = Camera.main;
            
            EnablePlay();
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
            
            _playerInput.Enable();

            _playerInput.Player.Play.performed += ctx => OnClick();
        }

        private void OnDisable() => _playerInput.Disable();
        
        public void SetProgressBarValue(float currentProgress) => _progressBar.value = currentProgress;
        public void SetMaxSliderValue(Vector3 startPos, Vector3 endPos) => _progressBar.maxValue = Vector2.Distance(startPos, endPos);
        public void EnablePlay() => _canPlay = true;

        private void Validate()
        {
            if (_progressBar == null)
                throw new NullReferenceException();
        }

        private void OnClick()
        {
            Ray ray = _camera.ScreenPointToRay(_playerInput.Player.Position.ReadValue<Vector2>());
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.TryGetComponent(out Enemy enemy) == false && _canPlay)
            {
                DisablePlay();
                Click?.Invoke();
            }
        }

        private void DisablePlay() => _canPlay = false;
    }
}
