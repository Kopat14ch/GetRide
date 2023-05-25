using Sources.Common;
using TMPro;
using UnityEngine;

namespace Sources.Views
{
    public class EnemyView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _timeToEndPoint;
        [SerializeField] private UI _ui;

        public UI UI => _ui;
        
        public void SetTimeToEndPoint(float time) => _timeToEndPoint.text = $"{time:F2}с";

        public void Enable()
        {
            _ui.gameObject.SetActive(true);
        }
        
        public void Disable()
        {
            _ui.gameObject.SetActive(false);
        }
    }
}