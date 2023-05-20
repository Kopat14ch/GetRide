using Sources.Models;
using Sources.Presenters;
using Sources.Views;
using UnityEngine;

namespace Sources.Setups
{
    [RequireComponent(typeof(PlayerView))]
    public class PlayerSetup : MonoBehaviour
    {
        [SerializeField] private Transform _endPoint;
        [SerializeField] private int _currentLevel;
        
        private Player _model;
        private PlayerView _view;
        private PlayerPresenter _presenter;

        private void Awake()
        {
            _view = GetComponent<PlayerView>();
            
            _model = new Player();
            _presenter = new PlayerPresenter(_model, _view);
        }

        private void OnEnable()
        {
            _presenter.Enable();
        }

        private void OnDisable()
        {
            _presenter.Disable();
        }
    }
}
