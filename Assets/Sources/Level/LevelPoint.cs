using UnityEngine;

namespace Sources.Level
{
    public class LevelPoint : MonoBehaviour
    {
        public Vector3 GetPosition => transform.position;

        public void ChangePosition()
        {
            transform.localPosition = new Vector3(0,0, -transform.localPosition.z);
        }
    }
}