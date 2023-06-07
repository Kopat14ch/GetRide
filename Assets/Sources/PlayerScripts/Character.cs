using Sources.Common;
using UnityEngine;

namespace Sources.PlayerScripts
{
    [RequireComponent(typeof(Movement))]
    public class Character : MonoBehaviour
    {
        public float Speed { get; private set; } = 4.5f;
        public Movement Movement => GetComponent<Movement>();
        
        public Vector3 LastPosition { get; private set; }

        public void SetLastPosition() => LastPosition = transform.position;

        public void AddSpeed(float value) => Speed += value;
    }
}