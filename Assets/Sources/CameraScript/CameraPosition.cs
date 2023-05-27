using System.Collections.Generic;
using Sources.Level;
using UnityEngine;

namespace Sources.CameraScript
{
    public class CameraPosition : MonoBehaviour
    {
        [SerializeField] private LevelGenerator _generator;

        private void Start() => transform.position = Positions.Values[_generator.RoadCount-1];

        private static class Positions
        {
            public static readonly List<Vector3> Values = new List<Vector3>
            {
                new Vector3(20,70,-40),
                new Vector3(25,70,-40),
                new Vector3(35,70,-40),
                new Vector3(45,80,-40),
                new Vector3(50,90,-40),
                new Vector3(55,100,-40)
            };
        }
    }
}