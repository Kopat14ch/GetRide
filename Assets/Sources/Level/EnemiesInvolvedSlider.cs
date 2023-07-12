using Sources.StringController;
using Sources.Views;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Level
{
    [RequireComponent(typeof(Slider))]
    public class EnemiesInvolvedSlider : MonoBehaviour
    {
        [Header(HeaderNames.Objects)]
        [SerializeField] private PlayerView _playerView;
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Image _fillImage;
        
        private const string SeparationElement = "/";

        private Slider _slider;
        private Color _redColor;
        private Color _greenColor;
        private int _maxEnemies;
        private int _currentEnemies;

        public void Initialize(int maxMoveEnemies)
        {
            _slider = GetComponent<Slider>();
            
            _redColor = Color.red;
            _greenColor = Color.green;
            _maxEnemies = maxMoveEnemies;
            _slider.maxValue = _maxEnemies;
            _fillImage.color = _greenColor;
            _currentEnemies = 0;

            TrySetSliderValue();
            SetText();
        }

        private void OnEnable() => _playerView.DraggingEnemy += AddCurrentEnemy;
        private void OnDisable() => _playerView.DraggingEnemy -= AddCurrentEnemy;

        private void AddCurrentEnemy()
        {
            _currentEnemies++;
            
            if (_currentEnemies > _maxEnemies)
            {
                _fillImage.color = _redColor;
                _text.color = _redColor;
            }
            
            TrySetSliderValue();
            
            SetText();
        }

        private void TrySetSliderValue()
        {
            if (_currentEnemies > _maxEnemies)
                return;

            _slider.value = _currentEnemies;
        }

        private void SetText() => _text.text = $"{_currentEnemies}{SeparationElement}{_maxEnemies}";
    }
}
