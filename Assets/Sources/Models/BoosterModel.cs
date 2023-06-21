using System;
using Sources.Boosters;
using Sources.Views;
using UnityEngine;

namespace Sources.Models
{
    public class BoosterModel
    {
        private readonly Booster _booster;
        private readonly ParticleSystem _particleSystem;
        private readonly PlayerView _playerView;
        private int _count;

        public event Action<int> CountChanged;

        public BoosterModel(Booster booster, ParticleSystem particleSystem, PlayerView playerView)
        {
            _booster = booster;
            _particleSystem = particleSystem;
            _playerView = playerView;
        }

        public void AddBoost()
        {
            _count++;
            
            CountChanged?.Invoke(_count);
        }

        public void Activate()
        {
            if (_count <= 0 || _playerView.CanPlay == false)
                return;

            _booster.Activate();
            _particleSystem.Play();
            _count--;
            
            CountChanged?.Invoke(_count);
        }
    }
}