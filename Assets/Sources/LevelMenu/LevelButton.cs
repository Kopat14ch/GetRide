using Sources.Common;
using Sources.Level;
using Sources.StringController;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.LevelMenu
{
    [RequireComponent(typeof(Button))]
    public class LevelButton : MonoBehaviour
    {
        [Header(HeaderNames.Objects)]
        [SerializeField] private TextMeshProUGUI _numberText;
        [SerializeField] private TextMeshProUGUI _scoreText;
        
        private Button _button;
        private int _number;

        private void Awake() => _button = GetComponent<Button>();

        private void OnEnable() => _button.onClick.AddListener(OnButtonClick);

        private void OnDisable() => _button.onClick.RemoveListener(OnButtonClick);

        public void SetNumber(int number)
        {
            _numberText.text = number.ToString();
            _number = number;
        }

        public void SetScore(int number)
        {
            int score = LevelConfig.Instance.GetScore(number);

            _scoreText.text = score == -1 ? "0" : score.ToString();
        }

        private void OnButtonClick() => AsyncLoadScene.Instance.Load(IJunior.TypedScenes.Level.LoadAsync(_number));
    }
}