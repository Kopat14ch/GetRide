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

        private Player _model;
        private PlayerPresenter _presenter;

        public PlayerView View => _view;

        private void Awake()
        {
            _model = new Player();
            _presenter = new PlayerPresenter(_model, _view);
        }

        private void OnEnable() => _presenter.Enable();
        
        private void OnDisable() => _presenter.Disable();

        public void Init(LevelPoint point, Character character, float timeToEndPoint = 4f)
        {
            character.Movement.Init(point, _view, timeToEndPoint);
        }
    }
}