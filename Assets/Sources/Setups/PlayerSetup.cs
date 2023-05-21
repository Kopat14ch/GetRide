using Sources.Level;
using Sources.Models;
using Sources.PlayerScripts;
using Sources.Presenters;
using Sources.Views;
using UnityEngine;

namespace Sources.Setups
{
    [RequireComponent(typeof(PlayerView))]
    [RequireComponent(typeof(Movement))]
    public class PlayerSetup : MonoBehaviour
    {
        private PlayerView _view;
        private Player _model;
        private PlayerPresenter _presenter;
        private Movement _movement;

        private void Awake()
        {
            _view = GetComponent<PlayerView>();
            _movement = GetComponent<Movement>();
            
            _model = new Player();
            _presenter = new PlayerPresenter(_model, _view);
        }

        private void OnEnable() => _presenter.Enable();
        
        private void OnDisable() => _presenter.Disable();

        public void SetMovementPoint(LevelPoint point)
        {
            _movement.Init(point);
        }
    }
}