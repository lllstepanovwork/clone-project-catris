using Catris.UI;
using Zenject;

namespace Catris.Loading
{
    public class LoadingStepUIInit : LoadingStepBase
    {
        private UIPanelController _panelController;
        
        [Inject]
        public void Construct(UIPanelController uiPanelController)
        {
            _panelController = uiPanelController;
        }

        public override void Enter()
        {
            _panelController.Init(Exit);
        }

        public override LoadingStep GetStepType()
        {
            return LoadingStep.UIInit;
        }
    }
}
