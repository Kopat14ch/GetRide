using Sources.Boosters;
using Sources.Views;
using UnityEngine;

namespace Sources.Common
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private BoostersList _boostersList;
        [SerializeField] private PlayerView _playerView;

        private void Awake()
        {
            foreach (var boosterView in _boostersList.BoosterViews)
                boosterView.Initialize();

            foreach (var boosterSetup in _boostersList.BoosterSetups)
                boosterSetup.Initialize(_playerView);
        }
    }
}
