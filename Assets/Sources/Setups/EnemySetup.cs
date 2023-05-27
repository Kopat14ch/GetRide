using Sources.Models;
using Sources.StringController;
using Sources.Views;
using UnityEngine;

namespace Sources.Setups
{
    public class EnemySetup : MonoBehaviour
    { 
        [Header(HeaderNames.Objects)]
        [SerializeField] private EnemyView _view;
        
        private EnemyModel _model;

        private void Awake()
        {
            _model = new EnemyModel();
        }

        public void Init(float timeToEndPoint)
        {
            _model.SetTimeToEndPoint(timeToEndPoint);
        }
    }
}