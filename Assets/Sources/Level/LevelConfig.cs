﻿using System;
using Agava.YandexGames;
using Sources.Common;
using Sources.StringController;
using UnityEngine;

namespace Sources.Level
{
    public class LevelConfig : MonoBehaviour
    {
        [Header(HeaderNames.Objects)]
        [SerializeField] private TextAsset _jsonFile;
        public static LevelConfig Instance { get; private set; }

        private LevelsConfig _jsonConfig;

        public void Initialize()
        {
            if (Instance == null)
            {
                transform.parent = null;
                DontDestroyOnLoad(gameObject);
                Instance = this;

                if (Saver.Instance.SaveData.LevelsConfig.Levels.Length > 0)
                {
                    _jsonConfig = Saver.Instance.SaveData.LevelsConfig;
                }
                else
                {
                    _jsonConfig = JsonUtility.FromJson<LevelsConfig>(_jsonFile.text);
                    Saver.Instance.SaveAllLevelsConfig(_jsonConfig);
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        public int GetRoads(int number)
        {
            if (TryGetJsonConfig(number, out Level jsonConfig))
                return jsonConfig.RoadsCount;
            
            return -1;
        }

        public int GetSeed(int number)
        {
            if (TryGetJsonConfig(number, out Level jsonConfig))
                return jsonConfig.Seed;

            return -1;
        }

        public int GetMaxEnemiesDragging(int number)
        {
            if (TryGetJsonConfig(number, out Level jsonConfig))
                return jsonConfig.MaxEnemiesDraggingCount;

            return -1;
        }

        public int GetScore(int number)
        {
            if (TryGetJsonConfig(number, out Level jsonConfig))
                return jsonConfig.Score;

            return -1;
        }

        public void SetScore(int score, int number, int scoreWithExcess)
        {
            int tempScore;
            
            if (TryGetJsonConfig(number, out Level jsonConfig))
            {
                if (jsonConfig.Score < score)
                {
                    if (PlayerAccount.HasPersonalProfileDataPermission)
                    {
                        if (jsonConfig.Score == scoreWithExcess)
                            tempScore = score - scoreWithExcess;
                        else
                            tempScore = score;
                        
                        Agava.YandexGames.Leaderboard.GetPlayerEntry(YandexGames.LeaderBoardName, result =>
                        {
                            Agava.YandexGames.Leaderboard.SetScore(YandexGames.LeaderBoardName, result.score += tempScore);
                        });
                    }

                    jsonConfig.Score = score;

                    Saver.Instance.SaveScore(number, jsonConfig);
                }
            }
        }

        private bool TryGetJsonConfig(int number, out Level outJsonConfig)
        {
            outJsonConfig = null;
            
            foreach (var jsonConfig in _jsonConfig.Levels)
            {
                if (jsonConfig.Number == number)
                {
                    outJsonConfig = jsonConfig; 
                    return true;
                }
            }
            
            return false;
        }
    }
    [Serializable]
    public class Level
    {
        public int Number;
        public int RoadsCount;
        public int Seed;
        public int MaxEnemiesDraggingCount;
        public int Score;
    }
        
    [Serializable]
    public class LevelsConfig
    {
        public Level[] Levels;
    }
}