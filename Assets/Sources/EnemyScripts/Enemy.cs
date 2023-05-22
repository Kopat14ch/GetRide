using Sources.Common;
using Sources.Level;
using Sources.Views;
using UnityEngine;

namespace Sources.EnemyScripts
{
    [RequireComponent(typeof(Movement))]
    public class Enemy : MonoBehaviour
    {
        public void Init(LevelPoint point, PlayerView view) => GetComponent<Movement>().Init(point, view);
    }
}
