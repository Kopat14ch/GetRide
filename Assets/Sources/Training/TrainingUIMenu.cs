using Sources.Common;
using Sources.StringController;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Training
{
    public class TrainingUIMenu : TrainingUI
    {
        [Header(HeaderNames.Objects)]
        [SerializeField] private GraphicRaycaster _buttonsRaycaster;
        [SerializeField] private Button _nextLevels;
        [SerializeField] private Button _previousLevels;
        [SerializeField] private Image _currentLevel;

        private void Awake()
        {
            if (Saver.Instance.SaveData.IsTrained)
            {
                Destroy(gameObject);
            }
            else
            {
                _nextLevels.interactable = false;
                _previousLevels.interactable = false;
                IsDisabled = false;
                _buttonsRaycaster.enabled = false;
                _currentLevel.gameObject.SetActive(false);

                TextIndex = 0;

                SetTrainingTexts();
            }
        }

        private void OnEnable() => NextButton.onClick.AddListener(SetTrainingTexts);

        private void OnDisable() => NextButton.onClick.RemoveListener(SetTrainingTexts);

        private void SetTrainingTexts()
        {
            if (TextIndex + 1 > TrainingTexts.Count)
                return;

            if (TextIndex == 0)
            {
                TrainingTexts[TextIndex++].gameObject.SetActive(true); ;
            }
            else
            {
                TrainingTexts[TextIndex - 1].gameObject.SetActive(false);
                TrainingTexts[TextIndex++].gameObject.SetActive(true);
            }

            if (TextIndex + 1 > TrainingTexts.Count)
            {
                NextButton.gameObject.SetActive(false);
                _buttonsRaycaster.enabled = true;
                _currentLevel.gameObject.SetActive(true);
            }
        }
    }
}
