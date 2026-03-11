using Catris.Game;
using Zenject;

namespace Catris.Loading
{
    public class LoadingStepComplete : LoadingStepBase
    {
        private GameManager _gameManager;
        private CatQueue _catQueue;
        
        [Inject]
        public void Construct(GameManager gameManager, CatQueue catQueue)
        {
            _catQueue = catQueue;
            _gameManager = gameManager;
        }
        
        public override void Enter()
        {
            _catQueue.Init(()=>
            {
                _gameManager.Init(Exit);
            });
        }

        public override LoadingStep GetStepType()
        {
            return LoadingStep.Complete;
        }
    }
}
