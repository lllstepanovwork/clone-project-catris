using Catris.Game;
using Zenject;

namespace Catris.UI 
{
    public class PausePanel : BaseUIPanel
    {
        private GameManager _gameManager;
        private UIPanelController _panelController;
        
        [Inject]
        public void Construct(GameManager gameManager, UIPanelController panelController)
        {
            _gameManager = gameManager;
            _panelController = panelController;
        }
        
        public void OnContinueButtonClick()
        {
            _panelController.ClosePanel(this, true);
        }

        public void OnRestartButtonClick()
        {
            _gameManager.Restart();
            _panelController.ClosePanel(this, true);
        }
    }
}
