using System;
using Sources.EnemyScripts;
using Sources.Level;
using Sources.StringController;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Sources.Views
{
    public class PlayerView : MonoBehaviour
    {
        [Header(HeaderNames.Objects)]
        [SerializeField] private Slider _progressBar;
        [SerializeField] private EndPanel _endPanel;

        private PlayerInput _playerInput;
        private Camera _camera;
        private bool _isUI;

        public event Action Click;

        public bool CanPlay { get; private set; }

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

        public void ShowEndPanel()
        {
            _endPanel.Show();
        }
        
        public void SetProgressBarValue(float currentProgress) => _progressBar.value = currentProgress;
        public void SetMaxSliderValue(Vector3 startPos, Vector3 endPos) => _progressBar.maxValue = Vector2.Distance(startPos, endPos);
        public void EnablePlay() => CanPlay = true;

        private void Validate()
        {
            if (_progressBar == null)
                throw new NullReferenceException();
        }

        private void OnClick()
        {
            Ray ray = _camera.ScreenPointToRay(_playerInput.Player.Position.ReadValue<Vector2>());
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                EnemyTransformation enemy = hit.collider.GetComponent<EnemyTransformation>();

                if (enemy == null && CanPlay)
                {
                    DisablePlay();
                    Click?.Invoke();
                }
            }
        }

        private void DisablePlay() => CanPlay = false;
    }
}
