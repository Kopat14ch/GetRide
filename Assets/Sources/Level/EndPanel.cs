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
        }

        public void Show()
        {
            gameObject.SetActive(true);
            Time.timeScale = 0f;
        }

        private void SetMenuScene()
        {
            
        }
    }
}