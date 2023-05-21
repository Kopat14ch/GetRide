using UnityEngine;

namespace Sources.Level.Roads
{
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
    }
}