using OleksiiStepanov.App;
using OleksiiStepanov.Game;
using DG.Tweening;

namespace OleksiiStepanov.Loading
{
    public class LoadingStep_AppInit : LoadingStepBase
    {
        public override void Enter()
        {
            //Init framerate, resolution, etc.
            DeviceManager.Instance.Init();

            //DOTween Settings
            DOTween.SetTweensCapacity(1250,312);

            Exit();
        }

        public override LoadingStep GetStepType()
        {
            return LoadingStep.AppInit;
        }
    }
}
