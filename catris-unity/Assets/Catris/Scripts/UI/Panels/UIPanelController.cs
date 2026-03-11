using UnityEngine;
using System;
using Catris.Utils;
using DG.Tweening;
using Zenject;

namespace Catris.UI
{
    public class UIPanelController : MonoBehaviour
    {
        [Header("Panels")]
        public WinPanel winPanel;
        public PausePanel pausePanel;
        public LoadingPanel loadingPanel;

        public void Init(Action onSuccess = null)
        {
            OpenPanel(loadingPanel);

            loadingPanel.Init(onSuccess);
        }

        public void OpenPanel(BaseUIPanel baseUIPanel, bool animate = false)
        {
            baseUIPanel.gameObject.SetActive(true);

            if (animate)
            {
                baseUIPanel.canvasGroup.alpha = 0f;
                baseUIPanel.canvasGroup.DOFade(1, 0.25f);
            }
        }

        public void ClosePanel(BaseUIPanel baseUIPanel, bool animate = false)
        {
            if (animate)
            {
                baseUIPanel.canvasGroup.alpha = 1f;
                baseUIPanel.canvasGroup.DOFade(0, 0.25f).OnComplete(() =>
                {
                    baseUIPanel.gameObject.SetActive(false);
                });
            }
            else
            {
                baseUIPanel.gameObject.SetActive(false);
            }
        }
    }

    public abstract class BaseUIPanel : MonoBehaviour
    {
        [Header("Base Panel Content")]
        public CanvasGroup canvasGroup;
    }
}