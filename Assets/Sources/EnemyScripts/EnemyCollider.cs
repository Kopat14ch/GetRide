using System;
using Sources.PlayerScripts;
using UnityEngine;

namespace Sources.EnemyScripts
{
    public class EnemyCollider : MonoBehaviour
    {
        private EnemyTransformation _enemyTransformation;

        public event Action CollisionACharacter;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Character character))
                CollisionACharacter?.Invoke();
        }
    }
}