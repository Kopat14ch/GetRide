using Sources.Models;
using Sources.Views;

namespace Sources.Presenters
{
    public class PlayerPresenter
    {
        private readonly PlayerModel _model;
        private readonly PlayerView _view;

        public PlayerPresenter(PlayerModel model, PlayerView view)
        {
            _model = model;
            _view = view;
        }

        public void Enable()
        {
            
        }

        public void Disable()
        {
            
        }
        

    }
}
