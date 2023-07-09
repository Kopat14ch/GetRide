using Sources.Boosters;
using Sources.Models;
using Sources.Views;

namespace Sources.Presenters
{
    public class BoosterPresenter
    {
        private readonly BoosterModel _model;
        private readonly BoosterView _view;

        public BoosterPresenter(BoosterModel model, BoosterView view)
        {
            _model = model;
            _view = view;
        }

        public void Enable()
        {
            _model.CountChanged += OnCountChanged;
            _model.VideoShowed += OnVideoShowed;

            _view.BoosterActivated += OnBoosterActivated;
            _view.VideoAwardReceived += OnVideoAwardReceived;
        }
        
        public void Disable()
        {
            _model.CountChanged -= OnCountChanged;
            _model.VideoShowed -= OnVideoShowed;
            
            _view.BoosterActivated -= OnBoosterActivated;
            _view.VideoAwardReceived -= OnVideoAwardReceived;
        }

        private void OnVideoShowed()
        {
            _view.ShowVideo();
        }

        private void OnVideoAwardReceived()
        {
            _model.AddBoost();
        }
        
        private void OnCountChanged(int count)
        {
            _view.SetCount(count);
        }

        private void OnBoosterActivated(Booster booster)
        {
            _model.TryActivate();
        }
    }
}