using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Sources.LevelMenu
{
    public class LevelButtons : MonoBehaviour
    {
        private void Awake()
        {
            List<LevelButton> numbers = GetComponentsInChildren<LevelButton>().ToList();

            foreach (var levelButton in numbers)
                levelButton.Initialize();
            
            for (int i = 0; i < numbers.Count; i++)
                numbers[i].SetNumber(i + 1);
        }
    }
}
