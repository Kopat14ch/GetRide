using DG.Tweening;
using Sources.EnemyScripts;
using Sources.Level;
using Sources.PlayerScripts;
using Sources.Views;
using UnityEngine;
using UnityEngine.Events;

namespace Sources.Common
{
    public class Movement : MonoBehaviour
    {
        private const float MoveToValue = 1.5f;

        private LevelPoint _endPoint;
        private Tweener _tweener;
        private float _timeToEndPoint;
        private float _currentPosition;

        public PlayerView PlayerView { get; private set; }
        public UnityAction MovedBack;

        private void OnDestroy() => PlayerView.Click -= Move;
        
        public void Init(LevelPoint point, PlayerView view, float timeToEndPoint)
        {
            _endPoint = point;
            PlayerView = view;
            
            if (TryGetComponent(out Character character)) 
                view.SetMaxSliderValue(transform.position, _endPoint.GetPosition);

            _timeToEndPoint = timeToEndPoint;
            _currentPosition = Vector3.Distance(transform.position, _endPoint.GetPosition);
            
            PlayerView.Click += Move;
        }

        public void MoveTo(Vector3 position, EnemyTransformation enemyTransformation = null, PlayerView playerView = null)
        {
            if (_tweener != null)
                _tweener.Kill();
            
            _tweener = transform.DOMove(position, MoveToValue);

            TryUpdateChangePosition();

            if (enemyTransformation != null)
            {
                _tweener.OnComplete(() => OnRestartEnemy(enemyTransformation));
            }
            else if (playerView != null)
            {
                _tweener.OnComplete(() => OnRestartPlayer(playerView));
                
                playerView.EnableGlitch();
            }
                
        }

        public void SetTimeToEndPoint(float value) => _timeToEndPoint = value;

        private void ChangePosition()
        {
            float progress = Vector3.Distance(transform.position, _endPoint.GetPosition);

            float result = _currentPosition - progress;

            PlayerView.SetProgressBarValue(result);
        }

        private void Move()
        {
            _tweener = transform.DOMove(_endPoint.GetPosition, _timeToEndPoint);

            if (TryGetComponent(out Character character) )
                _tweener.OnComplete(PlayerEnd);

            TryUpdateChangePosition();
        }

        private void TryUpdateChangePosition()
        {
            if (TryGetComponent(out Character character)) 
                _tweener.OnUpdate(ChangePosition);
        }

        private void PlayerEnd() => PlayerView.ShowEndPanel();

        private void OnRestartPlayer(PlayerView playerView)
        {
            playerView.EnablePlay();
            playerView.DisableGlitch();
        }
        
        private void OnRestartEnemy(EnemyTransformation enemyTransformation) => enemyTransformation.EnableDrag();
    }
}
