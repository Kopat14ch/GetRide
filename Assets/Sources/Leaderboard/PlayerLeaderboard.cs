using Sources.Common;
using TMPro;
using UnityEngine;

namespace Sources.Leaderboard
{
    public class PlayerLeaderboard : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _score;
        [SerializeField] private TextMeshProUGUI _rank;

        private const string GoldColorHex = "#FFD700";
        private const string SilverColorHex = "#C0C0C0";
        private const string BronzeColorHex = "#967444";
        
        private const int FirstRank = 1;
        private const int SecondRank = 2;
        private const int ThirdRank = 3;

        private Color _goldColor;
        private Color _silverColor;
        private Color _bronzeColor;

        public void Init(string playerName, int score, int rank)
        {
            ColorUtility.TryParseHtmlString(GoldColorHex, out _goldColor);
            ColorUtility.TryParseHtmlString(SilverColorHex, out _silverColor);
            ColorUtility.TryParseHtmlString(BronzeColorHex, out _bronzeColor);
            
            _name.text = playerName;
            _score.text = score.ToString();
            _rank.text = rank.ToString();

            TrySetColorRank(rank);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }

        public void Enable(Panel panel)
        {
            transform.SetParent(panel.transform);
            
            gameObject.SetActive(true);
        }
        
        private void TrySetColorRank(int rank)
        {
            switch (rank)
            {
                case FirstRank:
                    _rank.color = _goldColor;
                    break;
                
                case SecondRank:
                    _rank.color = _silverColor;
                    break;
                
                case ThirdRank:
                    _rank.color = _bronzeColor;
                    break;
                
                default:
                    _rank.color = Color.white;
                    break;
            }
        }
    }
}