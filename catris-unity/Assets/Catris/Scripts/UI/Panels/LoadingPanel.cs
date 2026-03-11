using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using Catris.Game;
using Zenject;

namespace Catris.UI
{
    public class LoadingPanel : BaseUIPanel
    {
        [Header("Content")]
        [SerializeField] private Button continueButton;
        [SerializeField] private CanvasGroup continueButtonCanvasGroup;
        [SerializeField] private CanvasGroup loadingBarCanvasGroup;
        [SerializeField] private Image barImage;
        [SerializeField] private float loadingTime;

        private UIPanelController _panelController;
        
        [Inject]
        public void Construct(UIPanelController panelController)
        {
            _panelController = panelController;
        }

        public void Init(Action onSuccess)
        {
            Sequence sequence = DOTween.Sequence();

            sequence
                .Append(barImage.DOFillAmount(1, loadingTime))
                .Append(loadingBarCanvasGroup.DOFade(0, 1f))
                .Join(continueButtonCanvasGroup.DOFade(1, 1f))
                .AppendCallback(()=>
                {
                    continueButton.interactable = true;
                });

            onSuccess?.Invoke();
        }

		public void OnAULLinkButtonClick()
		{
			Application.OpenURL(Constants.AppsuloveCatrisLink);
		}

        public void OnOSLinkButtonClick()
        {
            Application.OpenURL(Constants.OSLink);
        }
        
        public void OnContinueButtonClick()
        {
            _panelController.ClosePanel(this, true);
        }
    }
}
