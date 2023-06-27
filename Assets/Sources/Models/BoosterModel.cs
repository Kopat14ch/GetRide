using System;
using Agava.YandexGames;
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
        
        public int Count { get; private set; }

        public BoosterModel(Booster booster, ParticleSystem particleSystem, PlayerView playerView)
        {
            _booster = booster;
            _particleSystem = particleSystem;
            _playerView = playerView;

            Count = Saver.Instance.GetSavedBoosterCount(booster);
        }

        public void TryActivate()
        {
            if (_playerView.CanPlay == false)
                return;

            if (Count <= 0)
            {
                VideoAd.Show(onRewardedCallback: AddBoost);

                return;
            }
            
            _booster.Activate();
            _particleSystem.Play();
            Count--;
            
            Saver.Instance.SaveBooster(this, _booster);
            CountChanged?.Invoke(Count);
        }

        private void AddBoost()
        {
            Count++;
            Saver.Instance.SaveBooster(this, _booster);
            CountChanged?.Invoke(Count);
        }
    }
}