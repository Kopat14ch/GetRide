using Lean.Localization;
using Sources.StringController;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Sources.LevelMenu.Language
{
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(Button))]
    public class CurrentLanguage : MonoBehaviour
    {
        [Header(HeaderNames.Objects)]
        [SerializeField] private LeanLocalization _localization;
        [SerializeField] private Sprite _russianSprite;
        [SerializeField] private Sprite _englishSprite;
        [SerializeField] private Sprite _turkishSprite;

        private Image _image;
        private Button _button;

        public UnityAction CurrentLanguageClick;
        
        private void Awake()
        {
            _image = GetComponent<Image>();
            _button = GetComponent<Button>();
            
            SetLanguage();
        }

        private void OnEnable() => _button.onClick.AddListener(OnButtonClick);
        private void OnDisable() => _button.onClick.RemoveListener(OnButtonClick);

        public void SetLanguage()
        {
            if (_localization.CurrentLanguage == Languages.Russian.ToString())
                _image.sprite = _russianSprite;
            else if (_localization.CurrentLanguage == Languages.English.ToString())
                _image.sprite = _englishSprite;
            else if (_localization.CurrentLanguage == Languages.Turkish.ToString())
                _image.sprite = _turkishSprite;
        }
        
        public void Enable() => gameObject.SetActive(true);
        public void Disable() => gameObject.SetActive(false);

        private enum Languages
        {
            Russian,
            English,
            Turkish
        }

        private void OnButtonClick()
        {
            CurrentLanguageClick.Invoke();
        }
    }
}
