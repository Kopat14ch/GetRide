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
        private float _timeToEndPoint;
        private Tweener _tweener;
        
        private void OnDestroy()
        {
            _view.Click -= Move;
        }

        public void Init(LevelPoint point, PlayerView view, float timeToEndPoint)
        {
            _endPoint = point;
            _view = view;
            
            if (TryGetComponent(out Character character)) 
                view.SetMaxSliderValue(_endPoint.GetPosition, transform.position);

            _view.Click += Move;

            _timeToEndPoint = timeToEndPoint;
        }

        private void ChangePosition()
        {
            var progress = Vector2.Distance(_endPoint.GetPosition, transform.position) / 100;

            _view.SetProgressBarValue(progress);
        }

        private void Move()
        {
            _tweener = transform.DOMove(_endPoint.GetPosition, _timeToEndPoint);

            if (TryGetComponent(out Character character))
                _tweener.OnUpdate(ChangePosition);
        }
    }
}
