using Sources.Common;
using Sources.EnemyScripts;
using UnityEngine;

namespace Sources.PlayerScripts
{
    [RequireComponent(typeof(Movement))]
    [RequireComponent(typeof(Character))]
    public class PlayerCollider : MonoBehaviour
    {
        private Movement _movement;
        private Character _character;

        private void Awake()
        {
            _movement = GetComponent<Movement>();
            _character = GetComponent<Character>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Enemy enemy))
                _movement.MoveTo(_character.LastPosition, playerView: _movement.PlayerView);
        }
    }
}
