using DG.Tweening;
using Sources.Level;
using Sources.StringController;
using Sources.Views;
using UnityEngine;

namespace Sources.PlayerScripts
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private PlayerView _view;
        
        private Tweener _tweener;
        private LevelPoint _endPoint;
        private float _timeToEndPoint = 2f;

        private void OnEnable()
        {
            _view.Click += Move;
        }

        private void OnDisable()
        {
            _view.Click -= Move;
        }

        public void Init(LevelPoint point)
        {
            _endPoint = point;
        }

        private void Move()
        { 
            _tweener = transform.DOMove(_endPoint.GetPosition, _timeToEndPoint);
        }
    }
}
