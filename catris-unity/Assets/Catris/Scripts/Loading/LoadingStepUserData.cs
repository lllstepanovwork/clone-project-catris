using Catris.Game;

namespace Catris.Loading
{
    public class LoadingStepUserData : LoadingStepBase
    {
        public override void Enter()
        {
            Exit();
        }
        
        public override LoadingStep GetStepType()
        {
            return LoadingStep.UserData;
        }
    }
}
