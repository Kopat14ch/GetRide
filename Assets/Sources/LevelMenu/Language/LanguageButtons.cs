using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Sources.LevelMenu.Language
{
    public class LanguageButtons : MonoBehaviour
    {
        private List<LanguageButton> _languageButtons = new List<LanguageButton>();

        private void Awake()
        {
            _languageButtons = GetComponentsInChildren<LanguageButton>().ToList();

            foreach (var languageButton in _languageButtons)
            {
                languageButton.ButtonClicked += OnButtonClick;
            }
        }

        private void OnButtonClick(LanguageButton button)
        {
            foreach (var languageButton in _languageButtons)
            {
                if (languageButton == button)
                    languageButton.Enable();
                else
                    languageButton.Disable();
            }
        }
    }
}