using Sources.Bootstraps;
using Sources.Common;
using Sources.Level;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Sources.LevelMenu
{
    [RequireComponent(typeof(Button))]
    public class LevelButton : MonoBehaviour
    {
        [SerializeField] private LevelMenuBootstrap _levelMenuBootstrap;
        [SerializeField] private TextMeshProUGUI _numberText;
        [SerializeField] private TextMeshProUGUI _scoreText;
        
        private Button _button;
        private int _number;

        public UnityAction<int> ButtonClicked;

        public void Initialize()
        {
            _button = GetComponent<Button>();

            _button.onClick.AddListener(OnButtonClick);
        }

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

        private void OnButtonClick()
        {
            ButtonClicked?.Invoke(_number);

            AsyncLoadScene.Instance.Load(IJunior.TypedScenes.Level.LoadAsync(_levelMenuBootstrap));
        }
    }
}