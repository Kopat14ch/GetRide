using Sources.Settings;
using UnityEngine;

namespace Sources.Common
{
    public static class AdController
    {
        public static bool IsOpen;
        
        public static void OnOpenAd()
        {
            IsOpen = true;
            
            if (SettingsMenu.Instance.IsToggleMusicEnabled)
                SettingsMenu.Instance.DisableMusic();
            
            Time.timeScale = 0f;
        }

        public static void OnCloseAd()
        {
            Time.timeScale = 1f;
            
            IsOpen = false;
            
            if (SettingsMenu.Instance.IsToggleMusicEnabled)
                SettingsMenu.Instance.EnableMusic();
        }
    }
}