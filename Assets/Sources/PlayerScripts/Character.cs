using Sources.Common;
using UnityEngine;

namespace Sources.PlayerScripts
{
    [RequireComponent(typeof(Movement))]
    public class Character : MonoBehaviour
    {
        public float Speed { get; private set; } = 4.5f;

        private void Awake() => Movement = GetComponent<Movement>();
        
        public Movement Movement { get; private set; }
        
        public Vector3 LastPosition { get; private set; }

        public void SetLastPosition() => LastPosition = transform.position;

        public void AddSpeed(float value) => Speed += value;

        public void SetSpeed(float value) => Movement.SetTimeToEndPoint(value);
    }
}