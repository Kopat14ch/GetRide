using UnityEngine;

namespace Sources.Common
{
    public class VideoImage : MonoBehaviour
    {
        public void Enable() => gameObject.SetActive(true);
        public void Disable() => gameObject.SetActive(false);
    }
}
