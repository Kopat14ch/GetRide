using System;

namespace Sources.Models
{
    public class EnemyModel
    {
        private float _timeToEndPoint;

        public event Action<float> ChangeTime;

        public void SetTimeToEndPoint(float timeToEndPoint)
        {
            _timeToEndPoint = timeToEndPoint;
            
            ChangeTime?.Invoke(_timeToEndPoint);
        }
    }
}