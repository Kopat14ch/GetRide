using UnityEngine;

namespace Sources.LevelMenu
{
    public class Lock : MonoBehaviour
    {
        public void Enable() => gameObject.SetActive(true);

        public void Disable() => gameObject.SetActive(false);
    }
}
