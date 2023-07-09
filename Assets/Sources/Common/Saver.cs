using System;
using Agava.YandexGames;
using Sources.Boosters;
using Sources.Level;
using Sources.Models;
using UnityEngine;
using UnityEngine.Scripting;

namespace Sources.Common
{
    public class Saver : MonoBehaviour
    {
        public static Saver Instance { get; private set; }
        public static bool IsLoaded { get; private set; }
        public SaveData SaveData { get; private set; }

        public void Initialize()
        {
            if (Instance == null)
            {
                transform.parent = null;
                DontDestroyOnLoad(gameObject);
                Instance = this;
                IsLoaded = false;
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
            SaveData.MusicChanged = true;
            
            PlayerAccount.SetCloudSaveData(JsonUtility.ToJson(SaveData));
        }

        public void SaveScore(int numberLevel, Level.Level jsonConfig)
        {
            for (int i = 0; i < SaveData.LevelsConfig.Levels.Length; i++)
            { 
                if (SaveData.LevelsConfig.Levels[i].Number == numberLevel)
                {
                    SaveData.LevelsConfig.Levels[i] = jsonConfig;
                    PlayerAccount.SetCloudSaveData(JsonUtility.ToJson(SaveData));
                }
            }
        }

        public void SaveAllLevelsConfig(LevelsConfig levelsConfig)
        {
            SaveData.LevelsConfig = levelsConfig;
            
            PlayerAccount.SetCloudSaveData(JsonUtility.ToJson(SaveData));
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

        public void SaveShows(int shows)
        {
            SaveData.ShowsCount = shows;
            
            PlayerAccount.SetCloudSaveData(JsonUtility.ToJson(SaveData));
        }

        private void Load()
        {
            PlayerAccount.GetCloudSaveData(onSuccessCallback: OnLoaded);
        }

        private void OnLoaded(string jsonLoaded)
        {
            SaveData = JsonUtility.FromJson<SaveData>(jsonLoaded);
            IsLoaded = true;
        }
    }
    
    [Serializable]
    public class SaveData
    {
        [field: Preserve] public int ShowsCount;
        [field: Preserve] public int MagicTrafficLightCount;
        [field: Preserve] public int MagicPotionCount;
        [field: Preserve] public float MusicVolumeValue;
        [field: Preserve] public bool MusicChanged;
        [field: Preserve] public LevelsConfig LevelsConfig;
    }
}
