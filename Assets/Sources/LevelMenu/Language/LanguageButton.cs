using Lean.Localization;
using Sources.Common;
using Sources.StringController;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Sources.LevelMenu.Language
{
    [RequireComponent(typeof(Button))]
    public class LanguageButton : MonoBehaviour
    {
        [Header(HeaderNames.Objects)]
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
                EnableSuccessfully();
            else
                DisableSuccessfully();
        }

        private void OnEnable() => _button.onClick.AddListener(OnButtonClick);

        private void OnDisable() => _button.onClick.RemoveListener(OnButtonClick);

        public void Enable() => gameObject.SetActive(true);
        
        public void Disable() => gameObject.SetActive(false);
        
        public void EnableSuccessfully() => _successfully.Enable();

        public void DisableSuccessfully() => _successfully.Disable();

        private void OnButtonClick()
        {
            LeanLocalization.SetCurrentLanguageAll(LanguageSelect.ToString());
            ButtonClicked?.Invoke(this);
        }
    }
}
