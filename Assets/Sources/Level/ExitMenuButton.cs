using IJunior.TypedScenes;
using Sources.Common;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Level
{
    [RequireComponent(typeof(Button))]
    public class ExitMenuButton : MonoBehaviour
    {
        private Button _button;

        private void Awake() => _button = GetComponent<Button>();
        
        private void OnEnable() => _button.onClick.AddListener(OnButtonClick);

        private void OnDisable() => _button.onClick.RemoveListener(OnButtonClick);

        public void Enable() => gameObject.SetActive(true);

        public void Disable() => gameObject.SetActive(false);

        private void OnButtonClick()
        {
            Time.timeScale = 1f;
            
            AsyncLoadScene.Instance.Load(LevelsMenu.LoadAsync());
        }
    }
}
