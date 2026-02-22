using Assets.Scripts.Menu;
using Assets.Scripts.Menu.Levels;
using VContainer;
using VContainer.Unity;

namespace Assets.Scripts.DI
{
    public class MenuScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<MenuEnteryPoint>();
            builder.Register<SetupLevelSequenceConfig>(Lifetime.Singleton);
        }

    }
}
