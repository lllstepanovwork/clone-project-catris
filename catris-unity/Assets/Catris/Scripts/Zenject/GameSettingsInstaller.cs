using System;
using System.Collections.Generic;
using Catris.Game;
using UnityEngine;
using Zenject;

namespace Catris.Gameplay
{
    [CreateAssetMenu(menuName = "Catris/GameSettings")]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        public Cats cats;

        public override void InstallBindings()
        {
            Container.BindInstance(cats).IfNotBound();
        }
    }

    [Serializable]
    public class Cats
    {
        [Header("Content")] 
        public List<CatSO> catList;
    }
}