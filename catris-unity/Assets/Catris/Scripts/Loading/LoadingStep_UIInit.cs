using OleksiiStepanov.Game;
using OleksiiStepanov.UI;

namespace OleksiiStepanov.Loading
{
    public class LoadingStep_UIInit : LoadingStepBase
    {
        public override void Enter()
        {
            UIManager.Instance.Init(() =>
                {
                    AppLoader.Instance.LoaderCanvas.SetActive(false);

                    UIPanelController.Instance.Init(Exit);
                });
        }

        public override LoadingStep GetStepType()
        {
            return LoadingStep.UIInit;
        }
    }
}
