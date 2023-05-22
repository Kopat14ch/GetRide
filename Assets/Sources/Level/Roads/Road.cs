using UnityEngine;

namespace Sources.Level.Roads
{
    [RequireComponent(typeof(BoxCollider))]
    public class Road : MonoBehaviour
    {
        public LevelPoint Point { get; private set; }

        private void Awake()
        {
            Point = GetComponentInChildren<LevelPoint>();
        }

        public void ChangePoint()
        {
            Point.ChangePosition();
        }

        public Bounds ColliderBounds => GetComponent<BoxCollider>().bounds;
    }
}