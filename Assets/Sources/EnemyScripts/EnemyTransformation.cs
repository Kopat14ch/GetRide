using System.Collections;
using Sources.Common;
using Sources.Level;
using Sources.Level.Roads;
using Sources.Views;
using UnityEngine;

namespace Sources.EnemyScripts
{
    [RequireComponent(typeof(Movement))]
    [RequireComponent(typeof(EnemyCollider))]
    public class EnemyTransformation : MonoBehaviour
    {
        private const float MouseDragSpeed = 0.1f;
        
        private Camera _camera;
        private PlayerView _playerView;
        private PlayerInput _playerInput;
        private Road _currentRoad;
        private Vector3 _velocity;
        private bool _canDrag;

        public Vector3 LastPosition { get; private set; }
        public Movement Movement { get; private set; }
        public EnemyCollider Collider { get; private set; }

        private void Awake()
        {
            _camera = Camera.main;
            _playerInput = new PlayerInput();
            _velocity = Vector3.zero;
            _playerInput.Player.Drag.performed += ctx => TryDrag();

            Movement = GetComponent<Movement>();
            Collider = GetComponent<EnemyCollider>();
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
        }

        public void Rotate(float rotateEnemyValue)
        {
            transform.Rotate(0,rotateEnemyValue,0);
        }

        private void TryDrag()
        {
            Ray ray = _camera.ScreenPointToRay(_playerInput.Player.Position.ReadValue<Vector2>());
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.TryGetComponent(out EnemyTransformation enemy) && enemy == this && _canDrag)
                StartCoroutine(Drag());
        }

        private IEnumerator Drag()
        {
            float initialDistance = Vector3.Distance(transform.position,_camera.transform.position);

            while (_playerInput.Player.Drag.ReadValue<float>() != 0)
            {
                Ray ray = _camera.ScreenPointToRay(_playerInput.Player.Position.ReadValue<Vector2>());
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