using DG.Tweening;
using Sources.Level;
using Sources.PlayerScripts;
using Sources.Views;
using UnityEngine;

namespace Sources.Common
{
    public class Movement : MonoBehaviour
    {
        private PlayerView _view;
        private LevelPoint _endPoint;
        private Tweener _tweener;
        private float _timeToEndPoint;
        private float _currentPosition;

        private void OnDestroy()
        {
            _view.Click -= Move;
        }

        public void Init(LevelPoint point, PlayerView view, float timeToEndPoint)
        {
            _endPoint = point;
            _view = view;
            
            if (TryGetComponent(out Character character)) 
                view.SetMaxSliderValue(transform.position, _endPoint.GetPosition);

            _view.Click += Move;

            _timeToEndPoint = timeToEndPoint;

            _currentPosition = Vector3.Distance(transform.position, _endPoint.GetPosition);
        }

        private void ChangePosition()
        {
            float progress = Vector3.Distance(transform.position, _endPoint.GetPosition);

            float result = _currentPosition - progress;

            _view.SetProgressBarValue(result);
        }

        private void Move()
        {
            _tweener = transform.DOMove(_endPoint.GetPosition, _timeToEndPoint);

            if (TryGetComponent(out Character character))
                _tweener.OnUpdate(ChangePosition);
        }
    }
}
