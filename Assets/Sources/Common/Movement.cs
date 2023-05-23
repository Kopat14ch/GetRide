using DG.Tweening;
using Sources.Level;
using Sources.Views;
using UnityEngine;

namespace Sources.Common
{
    public class Movement : MonoBehaviour
    {
        private PlayerView _view;
        private LevelPoint _endPoint;
        private float _timeToEndPoint;
        
        private void OnDestroy()
        {
            _view.Click -= Move;
        }

        public void Init(LevelPoint point, PlayerView view, float timeToEndPoint)
        {
            _endPoint = point;
            _view = view;
            
            _view.Click += Move;

            _timeToEndPoint = timeToEndPoint;
        }
        private void Move() => transform.DOMove(_endPoint.GetPosition, _timeToEndPoint);
    }
}
