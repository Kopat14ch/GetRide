using System;
using Sources.Level.Roads;
using Sources.Setups;
using UnityEngine;

namespace Sources.Spawners
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private PlayerSetup _template;

        private PlayerSetup _player;

        public void Spawn(EndRoad endRoad, StartRoad startRoad)
        {
            Road tempRoad = startRoad.GetComponentInChildren<Road>();
            _player = Instantiate(_template, tempRoad.transform.localPosition, Quaternion.identity, tempRoad.transform);

            tempRoad = endRoad.GetComponentInChildren<Road>();
            _player.SetMovementPoint(tempRoad.Point);
        }
    }
}