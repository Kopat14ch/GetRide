using System.Collections.Generic;
using System.Linq;
using Sources.Level.Roads;
using Sources.Spawners;
using UnityEngine;

namespace Sources.Level
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField] private int _roadCount = 3;
        [SerializeField] private EnemySpawner _enemySpawner;
        [SerializeField] private Road _roadOneStripeTemplate;
        [SerializeField] private Road _roadNoStripe;
        [SerializeField] private CenterRoad _centerRoad;
        [SerializeField] private StartRoad _startRoad;
        [SerializeField] private EndRoad _endRoad;

        private RoadContainer[] _roadContainers;
        private List<Road> _roads;
        
        public IReadOnlyList<Road> Roads => _roads.GetRange(0, _roads.Count).AsReadOnly();
        
        private void Awake()
        {
            _roadContainers = GetComponentsInChildren<RoadContainer>();
            _roads = new List<Road>();
        }

        private void Start()
        {
            for (int i = 0; i < _roadCount; i++)
            {
                foreach (var roadContainer in _roadContainers)
                {
                    CreateRoad(roadContainer.gameObject, i == 0 ? _roadNoStripe : _roadOneStripeTemplate);
                }
                
                CreateRoad(_centerRoad.gameObject, _roadNoStripe, false);
            }
            
            _enemySpawner.Spawn(Roads, _centerRoad);
            CreateCenterRoad(_roadNoStripe,_centerRoad.GetComponentsInChildren<Road>().Last().GetComponent<Renderer>(), _startRoad.gameObject);
            CreateCenterRoad(_roadNoStripe, _centerRoad.GetComponentsInChildren<Road>().First().GetComponent<Renderer>(), _endRoad.gameObject, false);
        }

        private void CreateRoad(GameObject container, Road template, bool canAdd = true)
        {
            Road tempRoad;

            if (container == null) 
                return;

            if (container.GetComponentsInChildren<Road>().Length > 0)
            {
                Renderer containerRenderer = container.GetComponentsInChildren<Road>().Last().GetComponent<Renderer>();
                Vector3 tempPosition = GetNormalPosition(containerRenderer);
                
                Road road = Instantiate(template, tempPosition, Quaternion.identity, container.transform);
                tempRoad = road;
            }
            else
            {
                Road road = Instantiate(template, container.transform.position, Quaternion.identity, container.transform);
                
                tempRoad = road;
            }

            if (canAdd)
                _roads.Add(tempRoad);
        }
        
        private void CreateCenterRoad(Road template, Renderer objectRenderer, GameObject container, bool isRight = true)
        {
            Vector3 tempPosition = GetNormalPosition(objectRenderer, isRight);
            
            Road road = Instantiate(template, tempPosition, Quaternion.identity, container.transform);
        }

        private Vector3 GetNormalPosition(Renderer objectRenderer, bool isRight = true)
        {
            Vector3 tempPosition = objectRenderer.bounds.max;

            tempPosition.z = objectRenderer.transform.position.z;


            if (isRight)
                tempPosition.x += objectRenderer.bounds.extents.x;
            else 
                tempPosition.x = -(tempPosition.x + objectRenderer.bounds.extents.x);

            return tempPosition;
        }
    }
}