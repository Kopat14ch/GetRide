using System.Collections.Generic;
using System.Linq;
using Sources.Level.Roads;
using Sources.Setups;
using Sources.Spawners;
using Sources.StringController;
using UnityEngine;

namespace Sources.Level
{
    public class LevelGenerator : MonoBehaviour
    {
        [Header(HeaderNames.Objects)] 
        [SerializeField] private PlayerSpawner _playerSpawner;
        [SerializeField] private EnemiesSpawner _enemiesSpawner;
        [SerializeField] private Road _roadOneStripeTemplate;
        [SerializeField] private Road _roadNoStripe;
        [SerializeField] private Road _mediumRoad;
        [SerializeField] private CenterRoad _centerRoad;
        [SerializeField] private StartRoad _startRoad;
        [SerializeField] private EndRoad _endRoad;

        [Header(HeaderNames.Properties)] 
        [SerializeField] private int _roadCount;

        private RoadContainer[] _roadContainers;
        private List<Road> _roads;

        private IReadOnlyList<Road> Roads => _roads.GetRange(0, _roads.Count).AsReadOnly();
        
        private void Awake()
        {
            _roadContainers = GetComponentsInChildren<RoadContainer>();
            _roads = new List<Road>();
        }

        private void Start()
        {
            for (int i = 0; i < _roadCount; i++)
            {
                int valueToRotate = 0;

                foreach (var roadContainer in _roadContainers)
                {
                    if (valueToRotate > 0)
                    {
                        CreateRoad(roadContainer.gameObject, i == 0 ? _roadNoStripe : _roadOneStripeTemplate, true);
                    }
                    else
                    {
                        CreateRoad(roadContainer.gameObject, i == 0 ? _roadNoStripe : _roadOneStripeTemplate);
                        ++valueToRotate;
                    }
                }

                CreateRoad(_centerRoad.gameObject, _roadNoStripe, canAdd: false);
            }
            
            CreateCenterRoad(_mediumRoad,_centerRoad.GetComponentsInChildren<Road>().Last().GetComponent<Renderer>(), _startRoad.gameObject);
            CreateCenterRoad(_mediumRoad, _centerRoad.GetComponentsInChildren<Road>().First().GetComponent<Renderer>(), _endRoad.gameObject, false);
            
            PlayerSetup playerSetup = _playerSpawner.Spawn(_endRoad, _startRoad);
            _enemiesSpawner.Spawn(Roads, playerSetup.View);
        }

        private void CreateRoad(GameObject container, Road template, bool canChangePoint = false, bool canAdd = true)
        {
            Road road;

            if (container == null)
                return;

            if (container.GetComponentsInChildren<Road>().Length > 0)
            {
                Renderer containerRenderer = container.GetComponentsInChildren<Road>().Last().GetComponent<Renderer>();
                Vector3 tempPosition = GetNormalPosition(containerRenderer, template);

                road = Instantiate(template, tempPosition, Quaternion.identity, container.transform);

            }
            else
            {
                road = Instantiate(template, container.transform.position, Quaternion.identity, container.transform);
            }

            if (canChangePoint)
                road.ChangePoint();
            
            if (canAdd)
                _roads.Add(road);
        }
        
        private void CreateCenterRoad(Road template, Renderer objectRenderer, GameObject container, bool isRight = true)
        {
            Vector3 tempPosition = GetNormalPosition(objectRenderer, template, isRight);
            
            Road road = Instantiate(template, tempPosition, Quaternion.identity, container.transform);
        }

        private Vector3 GetNormalPosition(Renderer objectRenderer, Road template, bool isRight = true)
        {
            Bounds objectBounds = objectRenderer.bounds;
            Bounds templateBounds = template.GetComponent<Renderer>().bounds;
            Vector3 positionObject = objectBounds.max;

            positionObject.z = objectRenderer.transform.position.z;

            if (isRight)
                positionObject.x = objectBounds.max.x + templateBounds.max.x;
            else
                positionObject.x = objectBounds.min.x + templateBounds.min.x;

            return positionObject;
        }
    }
}