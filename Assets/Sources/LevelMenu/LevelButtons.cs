using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Sources.LevelMenu
{
    public class LevelButtons : MonoBehaviour
    {
        public int LevelNumber { get; private set; }
        
        public void Initialize()
        {
            List<LevelButton> levelButtons = GetComponentsInChildren<LevelButton>().ToList();


            for (int i = 0; i < levelButtons.Count; i++)
            {
                levelButtons[i].Initialize();
                levelButtons[i].SetNumber(i + 1);
                levelButtons[i].ButtonClicked += SetCurrentNumber;
            }
        }

        private void SetCurrentNumber(int number)
        {
            LevelNumber = number;
        }
    }
}
