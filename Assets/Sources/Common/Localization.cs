using Agava.YandexGames;
using Lean.Localization;
using UnityEngine;

namespace Sources.Common
{
    public class Localization : MonoBehaviour
    {
        private void Awake() => SetLanguageAll();

        private void SetLanguageAll()
        {
            switch (YandexGamesSdk.Environment.i18n.lang)
            {
                case "ru":
                    LeanLocalization.SetCurrentLanguageAll(Languages.Russian.ToString());
                    break;
                case "en":
                    LeanLocalization.SetCurrentLanguageAll(Languages.English.ToString());
                    break;
                case "tr":
                    LeanLocalization.SetCurrentLanguageAll(Languages.Turkish.ToString());
                    break;
            }
        }
    }
}
