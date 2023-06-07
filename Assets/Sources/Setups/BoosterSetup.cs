using System;
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
        private Booster _booster;
        private BoosterModel _model;
        private BoosterView _view;
        private BoosterPresenter _presenter;

        private void Awake()
        {
            _booster = GetComponent<Booster>();
            _view = GetComponent<BoosterView>();
            
            _model = new BoosterModel(_booster);
            _presenter = new BoosterPresenter(_model, _view);
        }

        private void OnEnable() => _presenter.Enable();

        private void OnDisable() => _presenter.Disable();
    }
}