using Assets.Scripts.Game.GridSystem;
using VContainer;
using VContainer.Unity;

namespace Assets.Scripts.DI
{
    public class GameScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<Grid>(Lifetime.Singleton);
        }

    }
}
