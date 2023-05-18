using System.Collections.Generic;
using System.Linq;
using Sources.Level.Roads;
using UnityEngine;

namespace Sources.Level
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField] private int _roadCount = 3;
        [SerializeField] private Road _roadTemplate;
        [SerializeField] private Road _roadNoStripe;
        [SerializeField] private CenterRoad _centerRoad;
        [SerializeField] private StartRoad _startRoad;
        [SerializeField] private EndRoad _endRoad;


        private RoadContainer[] _roadContainers;
        private List<Road> _roads = new List<Road>();

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
                    CreateRoad(roadContainer.gameObject, i == 0 ? _roadNoStripe : _roadTemplate);
                }
                
                CreateRoad(_centerRoad.gameObject, _roadNoStripe);
            }
            
            CreateRoad(_roadNoStripe,_centerRoad.GetComponentsInChildren<Road>().Last().GetComponent<Renderer>(), _startRoad.gameObject);
            CreateRoad(_roadNoStripe, _centerRoad.GetComponentsInChildren<Road>().First().GetComponent<Renderer>(), _endRoad.gameObject, false);

        }

        private void CreateRoad(GameObject container, Road template)
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
            
            _roads.Add(tempRoad);
        }
        
        private void CreateRoad(Road template, Renderer objectRenderer, GameObject container, bool isRight = true)
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