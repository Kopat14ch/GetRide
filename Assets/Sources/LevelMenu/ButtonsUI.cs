using UnityEngine;
using UnityEngine.UI;

namespace Sources.LevelMenu
{
    public class ButtonsUI : MonoBehaviour
    {
        [SerializeField] private Button _nextLevels;
        [SerializeField] private Button _previousLevels;
        [SerializeField] private LevelButtons _levelButtons;

        private void OnEnable()
        {
            _nextLevels.onClick.AddListener(_levelButtons.NextLevels);
            _previousLevels.onClick.AddListener(_levelButtons.PreviousLevels);
        }

        private void OnDisable()
        {
            _nextLevels.onClick.RemoveListener(_levelButtons.NextLevels);
            _previousLevels.onClick.RemoveListener(_levelButtons.PreviousLevels);
        }
    }
}
