using Agava.YandexGames;
using Sources.Common;
using Sources.Settings;
using Sources.StringController;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Leaderboard
{
    public class LeaderboardUI : MonoBehaviour
    {
        [Header(HeaderNames.Objects)]
        [SerializeField] private LeaderboardPool _pool;
        [SerializeField] private Button _toggleButton;
        [SerializeField] private Button _rejectButton;
        [SerializeField] private Button _acceptButton;
        [SerializeField] private Button _closeButton;
        [SerializeField] private Panel _panel;
        [SerializeField] private Panel _content;
        [SerializeField] private GameObject _authorized;
        [SerializeField] private GameObject _notAuthorized;

        private bool _canOpen;
        
        public static LeaderboardUI Instance { get; private set; }
        
        public void Initialize()
        {
            Enable();

            if (Instance == null)
            {
                transform.parent = null;
                DontDestroyOnLoad(gameObject);
                Instance = this;
                _canOpen = true;
                
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
            _closeButton.onClick.AddListener(Disable);
        }

        private void OnDisable()
        {
            _toggleButton.onClick.RemoveListener(Toggle);
            _rejectButton.onClick.RemoveListener(Disable);
            _acceptButton.onClick.RemoveListener(Accept);
            _closeButton.onClick.RemoveListener(Disable);
        }

        public void Disable()
        {
            Time.timeScale = 1f;
            
            _authorized.gameObject.SetActive(false);
            _notAuthorized.gameObject.SetActive(false);
            _pool.DisablePlayers();
            _panel.Disable();
        }

        public void SetCanOpen(bool value) => _canOpen = value;

        private void Toggle()
        {
            if (_panel.isActiveAndEnabled)
            {
                Disable();
            }
            else
            {
                Enable();
                SettingsMenu.Instance.DisablePanel();
            }
        }

        private void Enable()
        {
            if (_canOpen == false)
                return;

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
    }
}
