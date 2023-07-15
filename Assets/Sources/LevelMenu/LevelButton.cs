using Sources.Common;
using Sources.Level;
using Sources.StringController;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.LevelMenu
{
    [RequireComponent(typeof(Button))]
    public class LevelButton : MonoBehaviour
    {
        [Header(HeaderNames.Objects)]
        [SerializeField] private TextMeshProUGUI _numberText;
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private Lock _lock;

        private int _number;
        
        private Button Button => GetComponent<Button>();

        private void OnEnable() => Button.onClick.AddListener(OnButtonClick);

        private void OnDisable() => Button.onClick.RemoveListener(OnButtonClick);

        public void SetNumber(int number)
        {
            _numberText.text = number.ToString();
            _number = number;
        }

        public void SetScore(int number)
        {
            int score = LevelConfig.Instance.GetScore(number);

            _scoreText.text = score.ToString();
        }

        public void SetLock(bool value)
        {
            if (value)
            {
                _lock.Enable();
                Button.interactable = false;
            }
            else
            {
                _lock.Disable();
                Button.interactable = true;
            }
        }

        private void OnButtonClick() => AsyncLoadScene.Instance.Load(IJunior.TypedScenes.Level.LoadAsync(_number));
    }
}