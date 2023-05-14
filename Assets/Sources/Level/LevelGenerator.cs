using System.Collections.Generic;
using UnityEngine;

namespace Sources.Level
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField] private int _roadCount = 3;
        [SerializeField] private Road _roadTemplate;
        [SerializeField] private Road _roadEndTemplate;

        private const int RoadContainersCount = 2;
        private RoadContainer[] _roadContainers;
        private List<Road> _roads;

        private void Awake()
        {
            _roadContainers = new RoadContainer[RoadContainersCount];
            _roads = new List<Road>();
            
            _roadContainers = GetComponentsInChildren<RoadContainer>();
        }

        private void Start()
        {
            for (int i = 0; i < _roadCount; i++)
            {
                foreach (var roadContainer in _roadContainers)
                {
                    bool rotate = i + 1 >= _roadCount;
                    
                    CreateRoads(roadContainer,rotate);
                }
            }
        }

        private void CreateRoads(RoadContainer container ,bool end)
        {
            Road template = end ? _roadEndTemplate : _roadTemplate;

            Road road = Instantiate(template,container.transform);

            _roads.Add(road);
        }
    }
}