using Lean.Localization;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Sources.LevelMenu.Language
{
    [RequireComponent(typeof(Button))]
    public class LanguageButton : MonoBehaviour
    {
        [SerializeField] private LeanLocalization _localization;
        
        public Languages LanguageSelect;

        public UnityAction<LanguageButton> ButtonClicked;
        private Button _button;
        private Successfully _successfully;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _successfully = GetComponentInChildren<Successfully>();

            if (_localization.CurrentLanguage == LanguageSelect.ToString())
                Enable();
            else
                Disable();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClick);
        }

        public enum Languages
        {
            Russian,
            English,
            Turkish
        }

        public void Enable()
        {
            _successfully.Enable();
        }

        public void Disable()
        {
            _successfully.Disable();
        }

        private void OnButtonClick()
        {
            LeanLocalization.SetCurrentLanguageAll(LanguageSelect.ToString());
            ButtonClicked?.Invoke(this);
        }
    }
}
