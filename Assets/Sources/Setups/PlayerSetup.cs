using Sources.Level;
using Sources.PlayerScripts;
using Sources.StringController;
using Sources.Views;
using UnityEngine;

namespace Sources.Setups
{
    public class PlayerSetup : MonoBehaviour
    {
        [Header(HeaderNames.Objects)]
        [SerializeField] private PlayerView _view;
        
        public Character Character { get; private set; }
        
        public PlayerView View => _view;

        private void OnDestroy() => _view.Click -= Character.SetLastPosition;

        public void Init(LevelPoint point, Character character, int speedValue)
        {
            Character = character;
            Character.AddTimeToEndPoint(speedValue);
            Character.Movement.Init(point, _view, Character.TimeToEndPoint);
            
            _view.Click += Character.SetLastPosition;
        }
    }
}