using System.Collections.Generic;
using Sources.Level;
using Sources.StringController;
using UnityEngine;

namespace Sources.CameraScript
{
    public class CameraPosition : MonoBehaviour
    {
        [Header(HeaderNames.Objects)]
        [SerializeField] private LevelGenerator _generator;

        private void Start() => transform.position = Positions.Values[_generator.RoadCount-1];

        private static class Positions
        {
            public static readonly List<Vector3> Values = new List<Vector3>
            {
                new Vector3(20,70,-40),
                new Vector3(30,70,-38),
                new Vector3(35,80,-40),
                new Vector3(40,90,-40),
                new Vector3(50,95,-40),
                new Vector3(55,110,-40)
            };
        }
    }
}