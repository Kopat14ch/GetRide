using System;
using Sources.Boosters;
using Sources.Common;
using Sources.Views;
using UnityEngine;

namespace Sources.Models
{
    public class BoosterModel
    {
        private readonly Booster _booster;
        private readonly ParticleSystem _particleSystem;
        private readonly PlayerView _playerView;
        
        public event Action<int> CountChanged;
        public event Action VideoShowed;
        
        public int Count { get; private set; }

        public BoosterModel(Booster booster, ParticleSystem particleSystem)
        {
            _booster = booster;
            _particleSystem = particleSystem;

            Count = Saver.Instance.GetSavedBoosterCount(booster);
        }

        public void TryActivate()
        {
            if (Count <= 0)
            {
                VideoShowed?.Invoke();
                return;
            }
            
            _booster.Activate();
            _particleSystem.Play();
            Count--;
            
            Saver.Instance.SaveBooster(this, _booster);
            CountChanged?.Invoke(Count);
        }

        public void AddBoost()
        {
            Count++;
            Saver.Instance.SaveBooster(this, _booster);
            CountChanged?.Invoke(Count);
        }
    }
}