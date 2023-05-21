using DG.Tweening;
using Sources.Level;
using Sources.Views;
using UnityEngine;

namespace Sources.PlayerScripts
{
    public class Movement : MonoBehaviour
    {
        private PlayerView _view;
        private Tweener _tweener;
        private LevelPoint _endPoint;
        private float _timeToEndPoint = 2f;
        
        private void OnDestroy()
        {
            _view.Click -= Move;
        }

        public void Init(LevelPoint point, PlayerView view)
        {
            _endPoint = point;
            _view = view;
            
            _view.Click += Move;
        }

        private void Move()
        {
            _tweener = transform.DOMove(_endPoint.GetPosition, _timeToEndPoint);
        }
    }
}
