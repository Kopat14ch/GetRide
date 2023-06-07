using UnityEngine;

namespace Sources.Level.Roads
{
    [RequireComponent(typeof(BoxCollider))]
    public class Road : MonoBehaviour
    {
        private BoxCollider _boxCollider;
        
        public LevelPoint Point { get; private set; }
        
        public Bounds ColliderBounds => _boxCollider.bounds;

        private void Awake()
        {
            Point = GetComponentInChildren<LevelPoint>();
            _boxCollider = GetComponent<BoxCollider>();
        }

        public void ChangeChildPosition()
        {
            Vector3 colliderCenter = _boxCollider.center;
            colliderCenter.z = -colliderCenter.z;
            
            Point.ChangePosition();
            
            _boxCollider.center = colliderCenter;
        }
    }
}