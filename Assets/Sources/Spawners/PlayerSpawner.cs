﻿using Sources.Level.Roads;
using Sources.PlayerScripts;
using Sources.Setups;
using UnityEngine;

namespace Sources.Spawners
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private Character _template;
        [SerializeField] private PlayerSetup _playerSetup;

        private readonly Vector3 _rotateValue = new Vector3(0,-90,0);

        public PlayerSetup Spawn(EndRoad endRoad, StartRoad startRoad)
        {
            Road tempRoad = startRoad.GetComponentInChildren<Road>();

            Character character = Instantiate(_template, tempRoad.Point.GetPosition, Quaternion.identity);
            character.transform.Rotate(_rotateValue);

            tempRoad = endRoad.GetComponentInChildren<Road>();
            
            _playerSetup.Init(tempRoad.Point, character);

            return _playerSetup;
        }
    }
}