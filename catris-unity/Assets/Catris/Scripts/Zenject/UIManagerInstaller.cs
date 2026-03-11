using Catris.UI;
using UnityEngine;
using Zenject;

namespace Catris.Loading
{
    public class UIManagerInstaller : MonoInstaller
    {
        [Header("Content")]
        [SerializeField] private UIPanelController panelController;

        public override void InstallBindings()
        {
            Container.Bind<UIPanelController>()
                .FromInstance(panelController)
                .AsSingle();
            
        }
    }
}