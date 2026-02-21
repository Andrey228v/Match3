using Assets.Scripts.Animations;
using Assets.Scripts.SceneLoading;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Assets.Scripts.Boot
{
    public class RootScope : LifetimeScope
    {
        [SerializeField] private LoadingView _loadingView;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_loadingView); 

            builder.RegisterEntryPoint<BootEntryPoint>();
            builder.Register<IAsyncSceneLoading, AsyncSceneLoading>(Lifetime.Singleton);
            builder.Register<IAnimation, AnimationManager>(Lifetime.Singleton);
        }
    }
}
