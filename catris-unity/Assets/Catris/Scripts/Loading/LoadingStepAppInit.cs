using Catris.Game;
using DG.Tweening;

namespace Catris.Loading
{
    public class LoadingStepAppInit : LoadingStepBase
    {
        public override void Enter()
        {
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
