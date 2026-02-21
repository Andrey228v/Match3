using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.SceneLoading
{
    public class AsyncSceneLoading : IAsyncSceneLoading
    {   
        private readonly Dictionary<string, SceneInstance> _loaderScenes = new Dictionary<string, SceneInstance>();

        private LoadingView _loadingView;
        private CancellationTokenSource _cancellationTokenSource;

        public AsyncSceneLoading(LoadingView loadingView)
        {
            _loadingView = loadingView;
        }

        public void LoadingIsDone(bool value)
        {
            _loadingView.SetActiveScreen(value != true);
        }

        public async UniTask LoadScene(string sceneName)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            LoadingIsDone(false);
            await UniTask.Delay(TimeSpan.FromSeconds(2f), _cancellationTokenSource.IsCancellationRequested);
            var loadedScene = await Addressables.LoadSceneAsync(sceneName, LoadSceneMode.Additive).WithCancellation(_cancellationTokenSource.Token);
            SceneManager.SetActiveScene(loadedScene.Scene);

            if(_loaderScenes.ContainsKey(sceneName) == false)
            {
                _loaderScenes.Add(sceneName, loadedScene);
            }

            _cancellationTokenSource.Cancel();
        }

        public async UniTask UnLoadScene(string sceneName)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            var sceneInstance = _loaderScenes[sceneName];

            await Addressables.UnloadSceneAsync(sceneInstance).WithCancellation(_cancellationTokenSource.Token).AsUniTask();
            _loaderScenes.Remove(sceneName);
            _cancellationTokenSource.Cancel();
        }
    }
}
