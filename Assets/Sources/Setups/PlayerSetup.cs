using System;
using Sources.Level;
using Sources.Models;
using Sources.PlayerScripts;
using Sources.Presenters;
using Sources.StringController;
using Sources.Views;
using UnityEngine;

namespace Sources.Setups
{
    public class PlayerSetup : MonoBehaviour
    {
        [Header(HeaderNames.Objects)]
        [SerializeField] private PlayerView _view;

        private PlayerModel _model;
        private PlayerPresenter _presenter;
        private Character _character;

        public PlayerView View => _view;

        private void Awake()
        {
            _model = new PlayerModel();
            _presenter = new PlayerPresenter(_model, _view);
        }

        private void OnEnable() => _presenter.Enable();
        
        private void OnDisable() => _presenter.Disable();

        private void OnDestroy()
        {
            _view.Click -= _character.SetLastPosition;
        }

        public void Init(LevelPoint point, Character character, int addTimeCount)
        {
            for (int i = 0; i < addTimeCount; i++)
                _model.AddTime();

            _character = character;
            _character.Movement.Init(point, _view, _model.TimeToEndPoint);
            _view.Click += _character.SetLastPosition;
        }
    }
}