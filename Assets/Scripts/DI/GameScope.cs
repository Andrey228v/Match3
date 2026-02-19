using Assets.Scripts.Game.Board;
using Assets.Scripts.Game.GridSystem;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Assets.Scripts.DI
{
    public class GameScope : LifetimeScope
    {
        [SerializeField] private GameBoard _gameBoard;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<Game.GridSystem.Grid>(Lifetime.Singleton);
            builder.RegisterInstance(_gameBoard);
        }

    }
}
