using System.Collections.Generic;
using Sources.Common;
using Sources.Level;
using Sources.Views;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Training
{
    public class TrainingUILevel : TrainingUI
    {
        [SerializeField] private PlayerView _playerView;
        [SerializeField] private List<BoosterView> _boosterViews;
        [SerializeField] private List<Image> _boosterImages;

        private const int BoosterTextIndex = 2;
        
        private void Awake()
        {
            if (Saver.Instance.SaveData.IsTrained)
            {
                DisableBoosterImages();
                Destroy(gameObject);
            }
            else
            {
                DisableBoosterImages();
            
                SetTrainingTexts();
                _playerView.DisablePlay();
            }
        }

        private void OnEnable()
        {
            foreach (var boosterView in _boosterViews)
                boosterView.BoosterActivatedTraining += OnBoosterActivatedTraining;

            NextButton.onClick.AddListener(SetTrainingTexts);
        }

        private void OnDisable()
        {
            foreach (var boosterView in _boosterViews)
                boosterView.BoosterActivatedTraining -= OnBoosterActivatedTraining;
            
            NextButton.onClick.RemoveListener(SetTrainingTexts);
        }

        private void SetTrainingTexts()
        {
            if (TextIndex + 1 > TrainingTexts.Count)
            {
                _playerView.EnablePlay();
                IsDisabled = true;
                gameObject.SetActive(false);

                return;
            }

            if (TextIndex == 0)
            {
                TrainingTexts[TextIndex++].gameObject.SetActive(true); ;
            }
            else
            {
                if (TextIndex == BoosterTextIndex)
                {
                    foreach (var image in _boosterImages)
                        image.gameObject.SetActive(true);

                    _playerView.EnableActivateBoosterInTraining();
                    NextButton.interactable = false;
                }
                
                TrainingTexts[TextIndex - 1].gameObject.SetActive(false);
                TrainingTexts[TextIndex++].gameObject.SetActive(true);
            }
        }
        
        private void OnBoosterActivatedTraining()
        {
            NextButton.interactable = true;
            
            SetTrainingTexts();
            DisableBoosterImages();
        }

        private void DisableBoosterImages()
        {
            foreach (var image in _boosterImages)
                image.gameObject.SetActive(false);
        }
    }
}
