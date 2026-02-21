using Assets.Scripts.SceneLoading;
using DG.Tweening;
using UnityEngine;
using VContainer.Unity;

namespace Assets.Scripts.Boot
{
    public class BootEntryPoint : IInitializable
    {
        private IAsyncSceneLoading _sceneLoading;

        public BootEntryPoint(IAsyncSceneLoading sceneLoading)
        {
            _sceneLoading = sceneLoading;
        }

        public async void Initialize()
        {
            Application.targetFrameRate = 60;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            DOTween.SetTweensCapacity(5000, 100);
            await _sceneLoading.LoadScene(Scenes.MENU);
        }
    }
}
