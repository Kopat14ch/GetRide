using System;
using System.Collections;
using Sources.Common;
using Sources.Level;
using Sources.Level.Roads;
using Sources.Setups;
using Sources.Views;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Sources.EnemyScripts
{
    [RequireComponent(typeof(Movement))]
    [RequireComponent(typeof(EnemySetup))]
    [RequireComponent(typeof(EnemyCollider))]
    [RequireComponent(typeof(EnemyView))]
    public class Enemy : MonoBehaviour
    {
        private const float MouseDragSpeed = 0.1f;

        private EnemySetup _setup;
        private Camera _camera;
        private PlayerView _playerView;
        private PlayerInput _playerInput;
        private Road _currentRoad;
        private Vector3 _velocity;
        private bool _canDrag;

        public Vector3 LastPosition { get; private set; }

        public Movement Movement { get; private set; }
        public EnemyView View { get; private set; }
        public EnemyCollider Collider { get; private set; }

        private void Awake()
        {
            _camera = Camera.main;
            _playerInput = new PlayerInput();
            _velocity = Vector3.zero;
            _playerInput.Player.Drag.performed += ctx => TryDrag();
            
            _setup = GetComponent<EnemySetup>();
            Movement = GetComponent<Movement>();
            Collider = GetComponent<EnemyCollider>();
            View = GetComponent<EnemyView>();
        }

        private void OnEnable() => _playerInput.Enable();
        
        private void OnDisable() => _playerInput.Disable();
        
        private void OnDestroy() => _playerView.Click -= SetLastPosition;
        
        public void Init(LevelPoint point, PlayerView view, Road road, float timeToEndPoint)
        {
            _playerView = view;

            EnableDrag();
            _playerView.Click += SetLastPosition;

            Movement.Init(point, view, timeToEndPoint);
            _currentRoad = road;
            _setup.Init(timeToEndPoint, view);
        }

        public void Rotate(float rotateEnemyValue)
        {
            transform.Rotate(0,rotateEnemyValue,0);
        }

        private void TryDrag()
        {
            Ray ray = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.TryGetComponent(out Enemy enemy) && enemy == this && _canDrag)
                StartCoroutine(Drag());
        }

        private IEnumerator Drag()
        {
            float initialDistance = Vector3.Distance(transform.position,_camera.transform.position);
            
            while (_playerInput.Player.Drag.ReadValue<float>() != 0)
            {
                Ray ray = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());
                var position = transform.position;
                float positionZ = Vector3.SmoothDamp(position, ray.GetPoint(initialDistance), ref _velocity,
                    MouseDragSpeed).z;

                positionZ = Mathf.Clamp(positionZ, _currentRoad.ColliderBounds.min.z, _currentRoad.ColliderBounds.max.z);
                
                position.Set(position.x, position.y, positionZ);
                transform.position = position;
                
                yield return null;
            }
        }

        public void EnableDrag()
        {
            _canDrag = true;
            
            _playerView.Click += DisableDrag;
        }

        private void DisableDrag()
        {
            _canDrag = false;
            
            _playerView.Click -= DisableDrag;
        }
        
        private void SetLastPosition() => LastPosition = transform.position;
    }
}
