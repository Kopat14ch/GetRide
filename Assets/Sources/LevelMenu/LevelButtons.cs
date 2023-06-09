using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Sources.LevelMenu
{
    public class LevelButtons : MonoBehaviour
    {
        private List<LevelButton> _levelButtons = new List<LevelButton>();

        public const int MaxNumber = 30;
        private const int MinNumber = 0;

        private static int s_maxLevelNumber;
        private static int s_minLevelNumber;

        public void Initialize()
        {
            _levelButtons = GetComponentsInChildren<LevelButton>().ToList();
            
            s_maxLevelNumber = _levelButtons.Count;
            s_minLevelNumber = MinNumber;
            
            InitButtons();
        }


        public void NextLevels()
        {
            if (s_maxLevelNumber + _levelButtons.Count <= MaxNumber)
            {
                s_minLevelNumber = s_maxLevelNumber;
                s_maxLevelNumber += _levelButtons.Count;
                InitButtons();
            }
        }

        public void PreviousLevels()
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
                levelButton.SetNumber(buttonIndex + 1);
                levelButton.SetScore(buttonIndex + 1);

                buttonIndex++;
            }
        }
    }
}
