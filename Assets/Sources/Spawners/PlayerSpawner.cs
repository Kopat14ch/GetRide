using Sources.Level.Roads;
using Sources.PlayerScripts;
using Sources.Setups;
using UnityEngine;

namespace Sources.Spawners
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private Character _template;
        [SerializeField] private PlayerSetup _playerSetup;

        public PlayerSetup Spawn(EndRoad endRoad, StartRoad startRoad)
        {
            Road tempRoad = startRoad.GetComponentInChildren<Road>();

            Character character = Instantiate(_template, tempRoad.transform.localPosition, Quaternion.identity);

            tempRoad = endRoad.GetComponentInChildren<Road>();
            _playerSetup.Init(tempRoad.Point, character);

            return _playerSetup;
        }
    }
}