using Sources.Bootstraps;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Sources.LevelMenu
{
    public class LevelButton : MonoBehaviour
    {
        [SerializeField] private LevelMenuBootstrap _levelMenuBootstrap;

        private TextMeshProUGUI _textNumber;
        private Button _button;
        private int _number;

        public UnityAction<int> ButtonClicked;

        public void Initialize()
        {
            _button = GetComponent<Button>();
            _textNumber = GetComponentInChildren<TextMeshProUGUI>();
            
            _button.onClick.AddListener(OnButtonClick);
        }

        public void SetNumber(int number)
        {
            _textNumber.text = number.ToString();
            _number = number;
        }

        private void OnButtonClick()
        {
            ButtonClicked?.Invoke(_number);

            IJunior.TypedScenes.Level.Load(_levelMenuBootstrap);
        }
    }
}