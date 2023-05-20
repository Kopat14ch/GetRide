using DG.Tweening;
using Sources.Level;
using Sources.StringController;
using Sources.Views;
using UnityEngine;

namespace Sources.PlayerScripts
{
    [RequireComponent(typeof(PlayerView))]
    public class PlayerMovement : MonoBehaviour
    {
        [Header(HeaderNames.Objects)]
        [SerializeField] private LevelPoint _endPoint;
        
        [Header(HeaderNames.Properties)]
        [SerializeField] private float _timeToEndPoint;
        
        private PlayerView _view;
        private Tweener _tweener;

        private void Awake()
        {
            _view = GetComponent<PlayerView>();
        }

        private void OnEnable()
        {
            _view.Click += Move;
        }

        private void OnDisable()
        {
            _view.Click -= Move;
        }

        private void Move()
        { 
            _tweener = transform.DOMove(_endPoint.GetPosition, _timeToEndPoint);
        }
    }
}
