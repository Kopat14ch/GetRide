using System;
using Agava.YandexGames;
using Sources.Boosters;
using Sources.Models;
using UnityEngine;
using UnityEngine.Scripting;

namespace Sources.Common
{
    public class Saver : MonoBehaviour
    {
        public static Saver Instance;

        public SaveData SaveData { get; private set; }

        public void Initialize()
        {

            if (Instance == null)
            {
                transform.parent = null;
                DontDestroyOnLoad(gameObject);
                Instance = this;
                Load();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void SaveBooster(BoosterModel boosterModel, Booster booster)
        {
            switch (booster)
            {
                case MagicTrafficLight:
                    SaveData.MagicTrafficLightCount = boosterModel.Count;
                    break;
                
                case MagicPotion: 
                    SaveData.MagicPotionCount = boosterModel.Count;
                    break;
            }
            
            PlayerAccount.SetCloudSaveData(JsonUtility.ToJson(SaveData));
        }
        
        public void SaveMusicVolume(float value)
        {
            SaveData.MusicVolumeValue = value;
            SaveData.CanMusicChanged = true;
            
            PlayerAccount.SetCloudSaveData(JsonUtility.ToJson(SaveData));
        }

        public void Load()
        {
            PlayerAccount.GetCloudSaveData(onSuccessCallback: jsonData => SaveData = JsonUtility.FromJson<SaveData>(jsonData));
        }

        public int GetSavedBoosterCount(Booster booster)
        {
            switch (booster)
            {
                case MagicTrafficLight:
                    return SaveData.MagicTrafficLightCount;
                
                case MagicPotion:
                    return SaveData.MagicPotionCount;
            }

            return 0;
        }
    }
    
    [Serializable]
    public class SaveData
    {
        [field: Preserve] public int MagicTrafficLightCount;
        [field: Preserve] public int MagicPotionCount;
        [field: Preserve] public float MusicVolumeValue;
        [field: Preserve] public bool CanMusicChanged;
    }
}
