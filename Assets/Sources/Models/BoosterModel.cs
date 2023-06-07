using System;
using Sources.Boosters;

namespace Sources.Models
{
    public class BoosterModel
    {
        private readonly Booster _booster;
        private int _count;

        public event Action<int> CountChanged;

        public BoosterModel(Booster booster)
        {
            _booster = booster;
        }

        public void AddBoost()
        {
            _count++;
            
            CountChanged?.Invoke(_count);
        }

        public void Activate()
        {
            if (_count <= 0)
                return;
            
            _booster.Activate();
            _count--;
            
            CountChanged?.Invoke(_count);
        }
    }
}