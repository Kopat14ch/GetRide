using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.LevelMenu
{
    public class LevelButton : MonoBehaviour
    {
        private int _number;
        private TextMeshProUGUI _textNumber;
        private Button _button;

        public void Initialize()
        {
            _button = GetComponent<Button>();
            _textNumber = GetComponentInChildren<TextMeshProUGUI>();
            
            _button.onClick.AddListener(OnButtonClick);
        }

        public void SetNumber(int number)
        {
            _number = number;
            _textNumber.text = number.ToString();
        }

        private void OnButtonClick()
        {
            IJunior.TypedScenes.Level.Load(_number);
        }
    }
}