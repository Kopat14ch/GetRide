using Sources.Views;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Level
{
    [RequireComponent(typeof(Slider))]
    public class EnemiesInvolvedSlider : MonoBehaviour
    {
        [SerializeField] private PlayerView _playerView;
        [SerializeField] private TextMeshProUGUI _textMeshProUGUI;
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
                _textMeshProUGUI.color = _redColor;
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

        private void SetText() => _textMeshProUGUI.text = $"{_currentEnemies}{SeparationElement}{_maxEnemies}";
    }
}
