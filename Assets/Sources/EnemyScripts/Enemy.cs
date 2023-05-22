using System.Collections;
using Sources.Common;
using Sources.Level;
using Sources.Level.Roads;
using Sources.Views;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Sources.EnemyScripts
{
    [RequireComponent(typeof(Movement))]
    public class Enemy : MonoBehaviour
    {
        private const float MouseDragSpeed = 0.1f;
        
        private Camera _camera;
        private PlayerView _playerView;
        private PlayerInput _playerInput;
        private Road _currentRoad;
        private Vector3 _velocity;
        private bool _canDrag;

        private void Awake()
        {
            _camera = Camera.main;
            _playerInput = new PlayerInput();
            _playerInput.Player.Drag.performed += ctx => TryDrag();
            
            _velocity = Vector3.zero;
            _canDrag = true;
        }

        private void OnEnable()
        {
            _playerInput.Enable();
        }

        private void OnDisable()
        {
            _playerInput.Disable();
        }

        public void Init(LevelPoint point, PlayerView view, Road road)
        {
            _playerView = view;

            _playerView.Click += DisableDrag;
            
            GetComponent<Movement>().Init(point, view);
            _currentRoad = road;
            
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

        private void DisableDrag()
        {
            _canDrag = false;
            
            _playerView.Click -= DisableDrag;
        }
    }
}
