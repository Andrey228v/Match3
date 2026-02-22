using Assets.Scripts.Menu.Levels;
using Assets.Scripts.Menu.UI;
using Assets.Scripts.SceneLoading;
using VContainer.Unity;

namespace Assets.Scripts.Menu
{
    public class MenuEnteryPoint : IInitializable
    {
        private IAsyncSceneLoading _sceneLoading;
        private SetupLevelSequence _setupLevel;
        private LevelSequenceView _sequenceView;

        public MenuEnteryPoint(IAsyncSceneLoading sceneLoading, SetupLevelSequence setupLevel, LevelSequenceView sequenceView)
        {
            _sceneLoading = sceneLoading;
            _setupLevel = setupLevel;
            _sequenceView = sequenceView;
        }

        public async void Initialize()
        {
            await _setupLevel.Setup(7);
            _sequenceView.SetupButtonsView(3);
            _sceneLoading.LoadingIsDone(true);
        }
    }
}
