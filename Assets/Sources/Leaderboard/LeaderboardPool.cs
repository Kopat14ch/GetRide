using System.Collections.Generic;
using Agava.YandexGames;
using Sources.Common;
using Sources.StringController;
using UnityEngine;

namespace Sources.Leaderboard
{
    public class LeaderboardPool : MonoBehaviour
    {
        [Header(HeaderNames.Objects)]
        [SerializeField] private PlayerLeaderboard _prefabCurrentPlayer;
        [SerializeField] private PlayerLeaderboard _prefabPlayers;
        
        private const int PlayersCount = 6;

        private readonly List<PlayerLeaderboard> _playersLeaderboard = new List<PlayerLeaderboard>();

        private void Awake()
        {
            for (int i = 0; i < PlayersCount; i++)
            {
                PlayerLeaderboard tempPlayerLeaderboard = Instantiate(i == 0 ? _prefabCurrentPlayer : _prefabPlayers, Vector3.zero, Quaternion.identity, transform);
                
                _playersLeaderboard.Add(tempPlayerLeaderboard);
                tempPlayerLeaderboard.Disable();
            }
        }


        public void EnablePlayers(LeaderboardEntryResponse currentPlayer, LeaderboardEntryResponse[] leaders, Panel panel)
        {
            _playersLeaderboard[0].Init(currentPlayer.player.publicName, currentPlayer.score, currentPlayer.rank);
            _playersLeaderboard[0].Enable(panel);

            for (int i = 0; i < leaders.Length; i++)
            {
                _playersLeaderboard[i + 1].Init(leaders[i].player.publicName, leaders[i].score, leaders[i].rank);
                _playersLeaderboard[i + 1].Enable(panel);
            }
        }

        public void DisablePlayers()
        {
            foreach (var playerLeaderboard in _playersLeaderboard)
                playerLeaderboard.Disable();
        }
    }
}