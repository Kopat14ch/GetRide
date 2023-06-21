using UnityEngine;

namespace Sources.Boosters
{
    public abstract class Booster : MonoBehaviour
    {
        public abstract void Activate();

        public void Enable() => gameObject.SetActive(true);

        public void Disable() => gameObject.SetActive(false);
    }
}