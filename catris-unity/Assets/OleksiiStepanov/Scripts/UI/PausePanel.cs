using OleksiiStepanov.Game;

namespace OleksiiStepanov.UI 
{
    public class PausePanel : BaseUIPanel
    {
        public void OnContinueButtonClick()
        {
            UIPanelController.Instance.ClosePanel(UIPanelController.Instance.pausePanel, true);
        }

        public void OnRestartButtonClick()
        {
            GameManager.Instance.Restart();

            UIPanelController.Instance.ClosePanel(UIPanelController.Instance.pausePanel, true);
        }
    }
}
