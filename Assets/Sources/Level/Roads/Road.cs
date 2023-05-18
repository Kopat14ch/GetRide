using UnityEngine;
using UnityEngine.UI;

namespace Sources.Level.Roads {
    public class Road : MonoBehaviour
    {
        public void TryDeleteRectTransform()
        {
            if (TryGetComponent(out LayoutElement layoutElement) && TryGetComponent(out RectTransform rectTransform))
            {
                Destroy(layoutElement);
                Destroy(rectTransform);
            }
        }
    } 
}