using Sources.Level;
using Sources.Models;
using Sources.PlayerScripts;
using Sources.Presenters;
using Sources.Views;
using UnityEngine;

namespace Sources.Setups
{
    public class PlayerSetup : MonoBehaviour
    {
        [SerializeField] private PlayerView _view;
        [SerializeField] private Movement _movement;
        [SerializeField] private LevelPoint _endPoint;

        private Player _model;
        private PlayerPresenter _presenter;

        private void Awake()
        {
            _model = new Player();
            _presenter = new PlayerPresenter(_model, _view);
            _movement.Init(_endPoint);
        }

        private void OnEnable() => _presenter.Enable();
        
        private void OnDisable() => _presenter.Disable();
    }
}