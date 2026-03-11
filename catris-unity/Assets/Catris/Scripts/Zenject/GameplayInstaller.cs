using Catris.Game;
using UnityEngine;
using Zenject;

namespace Battleship.Gameplay
{
    public class GameplayInstaller : MonoInstaller
    {
        [Header("Content")]
        [SerializeField] private GameManager gameManager;
        [SerializeField] private CatQueue catQueue;
        
        public override void InstallBindings()
        {
            DeclareSignals();
            BindClasses();
        }

        private void DeclareSignals()
        {
        }

        private void BindClasses()
        {
            Container.Bind<GameManager>()
                .FromInstance(gameManager)
                 .AsSingle();
            
            Container.Bind<CatQueue>()
                .FromInstance(catQueue)
                .AsSingle();
        }
    }
}
