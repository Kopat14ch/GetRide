using System;
using System.Collections.Generic;
using System.Linq;
using Sources.StringController;
using UnityEngine;

namespace Sources.LevelMenu.Language
{
    public class LanguageButtons : MonoBehaviour
    {
        [Header(HeaderNames.Objects)]
        [SerializeField] private CurrentLanguage _currentLanguage;

        private List<LanguageButton> _languageButtons = new List<LanguageButton>();

        private void Awake() => _languageButtons = GetComponentsInChildren<LanguageButton>().ToList();

        private void Start() => Disable();

        private void OnEnable()
        {
            foreach (var languageButton in _languageButtons)
                languageButton.ButtonClicked += OnButtonClick;
            
            _currentLanguage.CurrentLanguageClick += Enable;
        }

        private void OnDisable()
        {
            foreach (var languageButton in _languageButtons)
                languageButton.ButtonClicked -= OnButtonClick;
            
            _currentLanguage.CurrentLanguageClick -= Enable;
        }

        private void Enable()
        {
            _currentLanguage.Disable();
            
            foreach (var language in _languageButtons)
                language.Enable();
        }

        private void Disable()
        {
            _currentLanguage.Enable();
            
            foreach (var language in _languageButtons)
                language.Disable();
        }

        private void OnButtonClick(LanguageButton button)
        {
            foreach (var languageButton in _languageButtons)
            {
                if (languageButton == button)
                    languageButton.EnableSuccessfully();
                else
                    languageButton.DisableSuccessfully();
            }

            _currentLanguage.SetLanguage();
            Disable();
        }
    }
}