using UnityEngine;
using UnityEngine.UI;
using OleksiiStepanov.Game;
using DG.Tweening;
using System;

namespace OleksiiStepanov.UI
{
    public class LoadingPanel : BaseUIPanel
    {
        [Header("Content")]
        [SerializeField] private Button continueButton;
        [SerializeField] private CanvasGroup continueButtonCanvasGroup;
        [SerializeField] private CanvasGroup loadingBarCanvasGroup;
        [SerializeField] private Image barImage;
        [SerializeField] private float loadingTime;

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

		public void OnLinkButtonClick()
		{
			Application.OpenURL(Constants.APPSULOVE_CATRIS_URL);
		}

        public void OnContinueButtonClick()
        {
            UIPanelController.Instance.ClosePanel(UIPanelController.Instance.loadingPanel, true);
        }
    }
}
