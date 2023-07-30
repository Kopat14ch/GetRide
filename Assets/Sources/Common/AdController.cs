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
            {
                SettingsMenu.Instance.DisableMusic();
                SettingsMenu.Instance.PauseSound();
            }
            
            Time.timeScale = 0f;
        }

        public static void OnCloseAd()
        {
            IsOpen = false;
            
            Time.timeScale = 1f;
            
            if (SettingsMenu.Instance.IsToggleMusicEnabled)
            {
                SettingsMenu.Instance.EnableMusic();
                SettingsMenu.Instance.UnPauseSound();
            }
        }
    }
}