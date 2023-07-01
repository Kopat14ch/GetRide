using IJunior.TypedScenes;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Level
{
    public class EndPanel : MonoBehaviour
    {
        [SerializeField] private Button _menuButton;

        private void Awake()
        {
            _menuButton.onClick.AddListener(SetMenuScene);
            
            gameObject.SetActive(false);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            Time.timeScale = 0f;
        }

        private void SetMenuScene()
        {
            Time.timeScale = 1f;

            LevelsMenu.Load();
        }
    }
}