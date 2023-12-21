using OleksiiStepanov.Game;

namespace OleksiiStepanov.Loading
{
    public class LoadingStep_Complete : LoadingStepBase
    {
        public override void Enter()
        {
            CatQueue.Instance.Init(()=>
            {
                GameManager.Instance.Init(Exit);
            });
        }

        public override LoadingStep GetStepType()
        {
            return LoadingStep.Complete;
        }
    }
}
