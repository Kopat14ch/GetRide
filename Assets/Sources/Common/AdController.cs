using Sources.Settings;
using UnityEngine;

namespace Sources.Common
{
    public static class AdController
    {
        public static void OnOpenAd()
        {
            Time.timeScale = 0f;
            SettingsMenu.Instance.DisableMusic();
        }

        public static void OnCloseAd()
        {
            SettingsMenu.Instance.EnableMusic();
            Time.timeScale = 1f;
        }
    }
}