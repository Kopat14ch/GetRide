using Sources.Models;
using Sources.Views;

namespace Sources.Presenters
{
    public class EnemyPresenter
    {
        private readonly EnemyModel _model;
        private readonly EnemyView _view;

        public EnemyPresenter(EnemyModel model, EnemyView view)
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