using Sources.Settings;
using UnityEngine;

namespace Sources.Common
{
    public static class AdController
    {
        public static void OnOpenAd()
        {
            if (SettingsMenu.Instance.IsToggleMusicEnabled)
                SettingsMenu.Instance.DisableMusic();
            
            Time.timeScale = 0f;
        }

        public static void OnCloseAd()
        {
            Time.timeScale = 1f;
            
            if (SettingsMenu.Instance.IsToggleMusicEnabled)
                SettingsMenu.Instance.EnableMusic();
        }
    }
}