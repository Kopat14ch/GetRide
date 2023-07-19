using System.Collections.Generic;
using Sources.Common;
using Sources.StringController;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Training
{
    public class TrainingUI : MonoBehaviour
    {
        [Header(HeaderNames.Objects)]
        [SerializeField] protected List<UI> TrainingTexts;
        [SerializeField] protected Button NextButton;
        
        protected int TextIndex;
        
        public bool IsDisabled { get; protected set; }
    }
}