using Assets.Scripts.Menu.Levels;
using Assets.Scripts.SceneLoading;
using VContainer.Unity;

namespace Assets.Scripts.Menu
{
    public class MenuEnteryPoint : IInitializable
    {
        private IAsyncSceneLoading _sceneLoading;
        private SetupLevelSequenceConfig _setupLevelSequenceConfig;

        public MenuEnteryPoint(IAsyncSceneLoading sceneLoading, SetupLevelSequenceConfig setupLevelSequenceConfig)
        {
            _sceneLoading = sceneLoading;
            _setupLevelSequenceConfig = setupLevelSequenceConfig;
        }

        public void Initialize()
        {
            _sceneLoading.LoadingIsDone(true);
        }
    }
}
