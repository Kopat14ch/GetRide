using UnityEngine;

namespace Sources.Level.Roads 
{
    public class Road : MonoBehaviour
    {
        public LevelPoint LevelPoint { get; private set; }

        private void Awake()
        {
            LevelPoint = GetComponentInChildren<LevelPoint>();
        }
    } 
}