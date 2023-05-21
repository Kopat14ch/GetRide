using UnityEngine;

namespace Sources.PlayerScripts
{
    [RequireComponent(typeof(Movement))]
    public class Character : MonoBehaviour
    {
        public Movement Movement => GetComponent<Movement>();
    }
}