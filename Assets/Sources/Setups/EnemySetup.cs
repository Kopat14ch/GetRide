using System;
using Sources.Models;
using Sources.Presenters;
using Sources.Views;
using UnityEngine;

namespace Sources.Setups
{
    public class EnemySetup : MonoBehaviour
    {
        [SerializeField] private EnemyView _view;
        
        private EnemyModel _model;
        private EnemyPresenter _presenter;
        private PlayerView _playerView;

        private void Awake()
        {
            _model = new EnemyModel();
            _presenter = new EnemyPresenter(_model, _view);
        }

        private void OnEnable() => _presenter.Enable();

        private void OnDisable() => _presenter.Disable();
        
        
        public void Init(float timeToEndPoint, PlayerView playerView)
        {

            _model.SetTimeToEndPoint(timeToEndPoint);
            _playerView = playerView;
        }
    }
}