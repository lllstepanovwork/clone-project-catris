using UnityEngine;
using Zenject;

namespace Catris.UI
{
    public class GameplayPanel : MonoBehaviour
    {
        private UIPanelController _panelController;
        
        [Inject]
        public void Construct(UIPanelController panelController)
        {
            _panelController = panelController;
        }
        
        public void OnPauseButtonClick()
        {
            _panelController.OpenPanel(_panelController.pausePanel, true);
        }
    }
}
