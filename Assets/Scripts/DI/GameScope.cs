using Assets.Scripts.Animations;
using Assets.Scripts.Game.Board;
using Assets.Scripts.Game.GridSystem;
using Assets.Scripts.Game.MatchTiles;
using Assets.Scripts.Game.Score;
using Assets.Scripts.Game.Tiles;
using Assets.Scripts.ResoursesLoading;
using Assets.Scripts.Utils;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Assets.Scripts.DI
{
    public class GameScope : LifetimeScope
    {
        [SerializeField] private GameBoard _gameBoard;
        [SerializeField] private GameResoursesLoader _loader;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_gameBoard);
            builder.RegisterInstance(_loader);
            builder.Register<MapGrid>(Lifetime.Singleton);
            builder.Register<SetupCamera>(Lifetime.Singleton);
            builder.Register<TilePool>(Lifetime.Singleton);
            builder.Register<GameDebug>(Lifetime.Singleton);
            builder.Register<BlankTileSetup>(Lifetime.Singleton);
            builder.Register<MatchFinder>(Lifetime.Singleton);
            builder.Register<GameProgress>(Lifetime.Singleton);
            builder.Register<ScoreCalculator>(Lifetime.Singleton);
        }

    }
}
