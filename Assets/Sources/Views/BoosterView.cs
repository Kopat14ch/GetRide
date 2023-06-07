using System;
using Sources.Boosters;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Views
{
    [RequireComponent(typeof(Booster))]
    public class BoosterView : MonoBehaviour
    {
        private Booster _booster;
        private Button _button;
        private TextMeshProUGUI _countText;

        public event Action<Booster> BoosterActivated;

        private void Awake()
        {
            _booster = GetComponent<Booster>();
            _button = GetComponentInChildren<Button>();
            _countText = GetComponentInChildren<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            try
            {
                Validate();
            }
            catch (Exception e)
            {
                enabled = false;
                throw e;
            }
            
            _button.onClick.AddListener(OnButtonClicked);
        }

        private void OnDisable() => _button.onClick.RemoveListener(OnButtonClicked);
        
        public void SetCount(int count)
        {
            if (_countText == null)
                return;
            
            _countText.text = count.ToString();
        }

        private void Validate()
        {
            if (_booster == null || _button == null)
                throw new NullReferenceException();
        }

        private void OnButtonClicked()
        {
            BoosterActivated?.Invoke(_booster);
        }
    }
}