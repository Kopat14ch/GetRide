using System;
using Agava.YandexGames;
using Sources.Common;
using Sources.StringController;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Leaderboard
{
    public class LeaderboardUI : MonoBehaviour
    {
        [SerializeField] private LeaderboardPool _pool;
        [SerializeField] private Button _toggleButton;
        [SerializeField] private Button _rejectButton;
        [SerializeField] private Button _acceptButton;
        [SerializeField] private Panel _panel;
        [SerializeField] private Panel _content;
        [SerializeField] private GameObject _authorized;
        [SerializeField] private GameObject _notAuthorized;

        private static LeaderboardUI s_instance;
        
        public void Initialize()
        {
            Enable();

            if (s_instance == null)
            {
                transform.parent = null;
                DontDestroyOnLoad(gameObject);
                s_instance = this;
                
                Disable();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void OnEnable()
        {
            _toggleButton.onClick.AddListener(Toggle);
            _rejectButton.onClick.AddListener(Disable);
            _acceptButton.onClick.AddListener(Accept);
        }

        private void OnDisable()
        {
            _toggleButton.onClick.RemoveListener(Toggle);
            _rejectButton.onClick.RemoveListener(Disable);
            _acceptButton.onClick.RemoveListener(Accept);
        }

        private void Toggle()
        {
            if (_panel.isActiveAndEnabled)
                Disable();
            else
                Enable();
        }

        private void Enable()
        {
            _panel.Enable();
            
            if (PlayerAccount.HasPersonalProfileDataPermission == false)
            {
                _notAuthorized.gameObject.SetActive(true);
            }
            else
            {
                Agava.YandexGames.Leaderboard.GetPlayerEntry(YandexGames.LeaderBoardName, currentPlayer =>
                {
                    Agava.YandexGames.Leaderboard.GetEntries(YandexGames.LeaderBoardName, players =>
                    {
                        _authorized.gameObject.SetActive(true);
                        _pool.EnablePlayers(currentPlayer, players.entries, _content);
                    });
                });
            }

            Time.timeScale = 0f;
        }

        private void Accept()
        {
            PlayerAccount.Authorize();
            PlayerAccount.RequestPersonalProfileDataPermission();
            Disable();
            Enable();
        }

        private void Disable()
        {
            Time.timeScale = 1f;
            
            _authorized.gameObject.SetActive(false);
            _notAuthorized.gameObject.SetActive(false);
            _pool.DisablePlayers();
            _panel.Disable();
        }
    }
}
