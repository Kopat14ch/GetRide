using System.Collections.Generic;
using System.Linq;
using Sources.Level;
using Sources.StringController;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.LevelMenu
{
    public class LevelButtons : MonoBehaviour
    {
        [Header(HeaderNames.Objects)]
        [SerializeField] private Button _nextLevels;
        [SerializeField] private Button _previousLevels;

        private readonly List<LevelButton> _levelButtons = new List<LevelButton>();

        public const int MaxNumber = 30;
        private const int MinNumber = 0;

        private static int s_maxLevelNumber;
        private static int s_minLevelNumber;
        
        private void OnEnable()
        {
            _nextLevels.onClick.AddListener(NextLevels);
            _previousLevels.onClick.AddListener(PreviousLevels);
        }

        private void OnDisable()
        {
            _nextLevels.onClick.RemoveListener(NextLevels);
            _previousLevels.onClick.RemoveListener(PreviousLevels);
        }

        public void Initialize()
        {
            _levelButtons.AddRange(GetComponentsInChildren<LevelButton>().ToList());

            s_maxLevelNumber = _levelButtons.Count;
            s_minLevelNumber = MinNumber;
            
            InitButtons();
        }

        private void NextLevels()
        {
            if (s_maxLevelNumber + _levelButtons.Count <= MaxNumber)
            {
                s_minLevelNumber = s_maxLevelNumber;
                s_maxLevelNumber += _levelButtons.Count;
                InitButtons();
            }
        }

        private void PreviousLevels()
        {
            if (s_minLevelNumber - _levelButtons.Count >= MinNumber)
            {
                s_maxLevelNumber = s_minLevelNumber;
                s_minLevelNumber -= _levelButtons.Count;
                InitButtons();
            }
        }

        private void InitButtons()
        {
            int buttonIndex = s_minLevelNumber;

            foreach (var levelButton in _levelButtons)
            {
                levelButton.SetLock(LevelConfig.Instance.GetLock(buttonIndex + 1));
                levelButton.SetNumber(buttonIndex + 1);
                levelButton.SetScore(buttonIndex + 1);
                
                buttonIndex++;
            }
        }
    }
}
