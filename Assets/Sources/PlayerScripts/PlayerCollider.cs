using UnityEngine;

namespace Sources.PlayerScripts
{
    [RequireComponent(typeof(Collider))]
    public class PlayerCollider : MonoBehaviour
    {
        private Collider _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            
        }
    }
}
