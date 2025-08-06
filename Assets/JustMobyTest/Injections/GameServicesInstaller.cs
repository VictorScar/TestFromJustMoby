using JustMobyTest.Core;
using UnityEngine;
using Zenject;

namespace JustMobyTest.Injections
{
    public class GameServicesInstaller : MonoInstaller
    {
        [SerializeField] private GameServices gameServicesPrefab;

        public override void InstallBindings()
        {
            var instance = Container.InstantiatePrefab(gameServicesPrefab);
            var gameService = instance.GetComponent<GameServices>();
            Container.Bind<GameServices>().FromInstance(gameService).AsSingle().NonLazy();
        }
    }
}