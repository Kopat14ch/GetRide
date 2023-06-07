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
        
        private Character _character;
        
        public PlayerView View => _view;

        private void OnDestroy() => _view.Click -= _character.SetLastPosition;

        public void Init(LevelPoint point, Character character, int speedValue)
        {
            _character = character;
            _character.AddSpeed(speedValue);
            _character.Movement.Init(point, _view, _character.Speed);
            _view.Click += _character.SetLastPosition;
        }
    }
}