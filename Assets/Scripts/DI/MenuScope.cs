using Assets.Scripts.Menu;
using Assets.Scripts.Menu.Levels;
using Assets.Scripts.Menu.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Assets.Scripts.DI
{
    public class MenuScope : LifetimeScope
    {
        [SerializeField] private LevelSequenceView _sequenceView;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<MenuEnteryPoint>();
            builder.Register<SetupLevelSequence>(Lifetime.Singleton);
            builder.RegisterInstance(_sequenceView);


        }
    }
}
