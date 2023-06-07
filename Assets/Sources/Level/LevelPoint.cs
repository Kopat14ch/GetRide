using UnityEngine;

namespace Sources.Level
{
    public class LevelPoint : MonoBehaviour
    {
        public Vector3 GetPosition => transform.position;

        public void ChangePosition() => transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, -transform.localPosition.z);
    }
}