using Sources.Common;
using UnityEngine;

namespace Sources.PlayerScripts
{
    [RequireComponent(typeof(Movement))]
    public class Character : MonoBehaviour
    {
        public float TimeToEndPoint { get; private set; } = 3.5f;

        private void Awake() => Movement = GetComponent<Movement>();
        
        public Movement Movement { get; private set; }
        
        public Vector3 LastPosition { get; private set; }

        public void SetLastPosition() => LastPosition = transform.position;

        public void AddTimeToEndPoint(float value) => TimeToEndPoint += value;

        public void SetTimeToEndPoint(float value) => Movement.SetTimeToEndPoint(value);
    }
}