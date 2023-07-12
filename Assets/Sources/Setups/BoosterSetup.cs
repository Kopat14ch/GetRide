using Sources.Boosters;
using Sources.Models;
using Sources.Presenters;
using Sources.Views;
using UnityEngine;

namespace Sources.Setups
{
    [RequireComponent(typeof(Booster))]
    public class BoosterSetup : MonoBehaviour
    {
        private ParticleSystem _particleSystem;
        private Booster _booster;
        private BoosterModel _model;
        private BoosterView _view;
        private BoosterPresenter _presenter;

        public void Initialize(PlayerView playerView)
        {
            _booster = GetComponent<Booster>();
            _particleSystem = GetComponentInChildren<ParticleSystem>();
            _view = GetComponent<BoosterView>();
            _model = new BoosterModel(_booster, _particleSystem, playerView);

            _presenter = new BoosterPresenter(_model, _view);
        }

        private void OnEnable() => _presenter.Enable();

        private void OnDisable() => _presenter.Disable();
    }
}