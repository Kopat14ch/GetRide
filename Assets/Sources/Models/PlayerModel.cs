using System;

namespace Sources.Models
{
    public class PlayerModel
    {
        private const float DefaultTime = 4f;
        public float TimeToEndPoint { get; private set; }
        public event Action<float> TimeChanged;

        public PlayerModel()
        {
            TimeToEndPoint = DefaultTime;
        }

        public void AddTime()
        {
            TimeToEndPoint++;
            TimeChanged?.Invoke(TimeToEndPoint);
        }
    }
}
