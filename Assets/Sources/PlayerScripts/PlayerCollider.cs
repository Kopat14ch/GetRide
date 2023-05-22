using Sources.EnemyScripts;
using UnityEngine;

namespace Sources.PlayerScripts
{
    public class PlayerCollider : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Enemy enemy))
                Time.timeScale = 0f;
        }
    }
}
