using System;
using Agava.YandexGames;
using Kino;
using Sources.Common;
using Sources.EnemyScripts;
using Sources.Level;
using Sources.Settings;
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
        [SerializeField] private AnalogGlitch _glitch;

        private PlayerInput _playerInput;
        private Camera _camera;
        private int _maxMoveEnemies;
        private int _enemyMoveCount;
        private bool _isUI;

        public event Action Click;
        public event Action DraggingEnemy;

        public bool CanPlay { get; private set; }
        public bool CanActivateBoosterInTraining { get; private set; }

        public void Initialize(int maxMoveEnemies)
        {
            CanActivateBoosterInTraining = Saver.Instance.SaveData.IsTrained;

            _playerInput = new PlayerInput();
            _camera = Camera.main;
            _maxMoveEnemies = maxMoveEnemies;

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

        private void Update() => _isUI = EventSystem.current.IsPointerOverGameObject();

        public void ShowEndPanel()
        {
            bool isExcess = _enemyMoveCount > _maxMoveEnemies;

            _endPanel.Show(_maxMoveEnemies, _enemyMoveCount, isExcess);
        }

        public void EnableGlitch()
        {
            SoundController.Instance.PlayGlitch();
            _glitch.enabled = true;
        }

        public void DisableGlitch()
        {
            SoundController.Instance.StopPlay();
            _glitch.enabled = false;
        }
        
        public void SetProgressBarValue(float currentProgress) => _progressBar.value = currentProgress;
        public void SetMaxSliderValue(Vector3 startPos, Vector3 endPos) => _progressBar.maxValue = Vector2.Distance(startPos, endPos);
        public void EnablePlay() => CanPlay = true;
        public void DisablePlay() => CanPlay = false;
        public void EnableActivateBoosterInTraining() => CanActivateBoosterInTraining = true;
        private void Validate()
        {
            if (_progressBar == null)
                throw new NullReferenceException();
        }

        private void OnClick()
        {
            Ray ray = _camera.ScreenPointToRay(_playerInput.Player.Position.ReadValue<Vector2>());
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && _isUI == false)
            {
                EnemyTransformation enemy = hit.collider.GetComponent<EnemyTransformation>();
                
                if (enemy == null && CanPlay)
                {
                    DisablePlay();
                    Click?.Invoke();
                }
                else if (enemy != null && CanPlay)
                {
                    DraggingEnemy?.Invoke();
                    _enemyMoveCount++;
                }
            }
        }
    }
}
